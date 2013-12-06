using System.Collections.Generic;
using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.Authentication;
using Woozle.Core.Model.Authentication;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Services.Stack.ServiceModel.Mandator;

namespace Woozle.Core.Services.Stack.Impl.Authentication
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
        /// <param name="mandator"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<ServiceModel.Mandator.Mandator> Get(MandatorsForSelection mandators)
        {
            var session = (Session)base.Request.GetSession();

            
            var loginUser = this.authenticationLogic.GetLoginUser(
                                                  session.SessionObject.User.Username,
                                                  session.SessionObject.User.Password);

            return Mapper.Map<IEnumerable<Woozle.Model.Mandator>, List<ServiceModel.Mandator.Mandator>>(loginUser.Mandators);
        }

        [ExceptionCatcher]
        public bool Post(MandatorSelect mandators)
        {
            var session = (Session) base.Request.GetSession();

            var mappedMandator = Mapper.Map<ServiceModel.Mandator.Mandator, Woozle.Model.Mandator>(
                mandators.SelectedMandator);

            //Login with the selected Mandator
            var result = this.authenticationLogic.Login(new LoginRequest
                                                            {
                                                                Username = session.SessionObject.User.Username,
                                                                Password = session.SessionObject.User.Password,
                                                                Mandator = mappedMandator
                                                            });

            if (result.LoginSuccessful)
            {
                //Set session object with the login result
                session.SessionObject = result.SessionData;

                //Save the new session (with the mandator information)
                this.SaveSession(session);

                return true;
            }

            return false;
        }
    }
}
