using System.Collections.Generic;
using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Authentication;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Services.Mandator;

namespace Woozle.Services.Authentication
{
    [Authenticate]
    public class MandatorSelectionService : AbstractService
    {
        private readonly IAuthenticationLogic authenticationLogic;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="authenticationLogic"><see cref="IAuthenticationLogic"/></param>
        public MandatorSelectionService(
            IAuthenticationLogic authenticationLogic)
        {
            this.authenticationLogic = authenticationLogic;
        }

        /// <summary>
        /// Gets the mandators of the currently logged in user
        /// </summary>
        /// <param name="mandators"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<Mandator.Mandator> Get(MandatorsForSelection mandators)
        {
            var session = (Session)base.Request.GetSession();

            
            var loginUser = this.authenticationLogic.GetLoginUser(session.SessionData.User.Username);

            return Mapper.Map<IEnumerable<Model.Mandator>, List<Mandator.Mandator>>(loginUser.Mandators);
        }

        [ExceptionCatcher]
        public bool Post(MandatorSelect mandators)
        {
            var session = (Session) base.Request.GetSession();

            var mappedMandator = Mapper.Map<Mandator.Mandator, Model.Mandator>(
                mandators.SelectedMandator);

            //Login with the selected Mandator
            var result = this.authenticationLogic.LoginMandator(session.SessionData.User.Username, mappedMandator);

            if (result.LoginSuccessful)
            {
                //Set session object with the login result
                session.SessionData = result.SessionData;

                //Save the new session (with the mandator information)
                this.SaveSession(session);

                return true;
            }

            return false;
        }
    }
}
