using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Authentication;
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
        /// <see cref="IAuthenticationLogic"/>
        /// </summary>
        private readonly IAuthenticationLogic authenticationLogic;

        /// <summary>
        /// <see cref="SessionData"/>
        /// </summary>
        private SessionData data;

        /// <summary>
        /// <see cref="LoginResult"/>
        /// </summary>
        private LoginResult loginResult;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="authenticationLogic"><see cref="IAuthenticationLogic"/></param>
        public WoozleCredentialsAuthProvider(IAuthenticationLogic authenticationLogic)
        {
            this.authenticationLogic = authenticationLogic;
        }

        /// <summary>
        /// <see cref="CredentialsAuthProvider.TryAuthenticate"/>
        /// </summary>
        public override bool TryAuthenticate(IServiceBase authService, string userName,
                                             string password)
        {
            //Try to login with the simple credentials (username + password)
            loginResult = this.authenticationLogic.Login(new LoginRequest
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
                    data = new SessionData(new User
                                               {
                                                   Username = userName,
                                                   Password = password
                                               }, null);
                }
            }
            else
            {
                //The login was successfully (only one mandator is assigned).
                loginSuccessful = true;
                data = loginResult.SessionData;
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
                se.SessionObject = data;
                authService.SaveSession(session, new TimeSpan(0, 1, 0, 0));
            }
        }
    }
}
