using System;
using System.Collections.Generic;
using Funq;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;

namespace Woozle.Services.Authentication
{
    /// <summary>
    /// Specific provider for authentication
    /// </summary>
    public class WoozleCredentialsAuthProvider : CredentialsAuthProvider
    {
        /// <summary>
        /// <see cref="SessionData"/>
        /// </summary>
        protected SessionData sessionData;

        /// <summary>
        /// <see cref="Container">IoC-Container</see>
        /// </summary>
        protected readonly Container container;

        /// <summary>
        /// ctor.
        /// </summary>
        public WoozleCredentialsAuthProvider(Container container)
        {
            this.container = container;
        }

        /// <summary>
        /// <see cref="CredentialsAuthProvider.TryAuthenticate"/>
        /// </summary>
        public override bool TryAuthenticate(IServiceBase authService, string userName,
                                             string password)
        {
            var authenticationLogic = container.LazyResolve<IAuthenticationLogic>();

            //Try to login with the simple credentials (username + password)
            var loginResult = authenticationLogic().Login(new LoginRequest
                                                            {
                                                                Username = userName,
                                                                Password = password
                                                            });
            var loginSuccessful = false;


            //Check the login result
            if (!loginResult.LoginSuccessful)
            {
                if (loginResult.CheckMandators)
                {
                    //There are more than one assigned mandator => MandatorSelection is required.
                    loginSuccessful = true;
                    sessionData = new SessionData(new User
                                               {
                                                   Username = userName,
                                               }, null);
                }
            }
            else
            {
                //The login was successfully (only one mandator is assigned).
                loginSuccessful = true;
                sessionData = loginResult.SessionData;
                sessionData.Roles = container.Resolve<IGetRolesLogic>().GetUserRoles(sessionData);
            }

            return loginSuccessful;
        }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens,
                                             Dictionary<string, string> authInfo)
        {
            //Save the authentication information after successful authentication.
            var se = session as Session;
            if (se != null)
            {
                se.SessionData = sessionData;
                authService.SaveSession(session, new TimeSpan(0, 1, 0, 0));
            }
        }
    }
}
