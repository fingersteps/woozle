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
        /// <param name="mandator"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<Mandator.MandatorDto> Get(MandatorsForSelectionDto mandators)
        {
            var session = (Session)base.Request.GetSession();

            
            var loginUser = this.authenticationLogic.GetLoginUser(
                                                  session.SessionObject.User.Username,
                                                  session.SessionObject.User.Password);

            return Mapper.Map<IEnumerable<Model.Mandator>, List<MandatorDto>>(loginUser.Mandators);
        }

        [ExceptionCatcher]
        public bool Post(MandatorSelectDto mandators)
        {
            var session = (Session) base.Request.GetSession();

            var mappedMandator = Mapper.Map<MandatorDto, Model.Mandator>(
                mandators.SelectedMandatorDto);

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
