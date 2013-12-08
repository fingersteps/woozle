using System;
using System.Linq;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Persistence.Repository;

namespace Woozle.Domain.Authentication
{
    /// <summary>
    /// Contains authentication related logic.
    /// </summary>
    /// <remarks></remarks>
    public class AuthenticationLogic : AbstractLogic, IAuthenticationLogic
    {
        /// <summary>
        /// <see cref="IUserRepository"/>
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="userRepository"><see cref="IUserRepository"/></param>
        public AuthenticationLogic(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #region IAuthenticationLogic Members

        /// <summary>
        /// Performs the login with the given request information.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public LoginResult Login(LoginRequest loginRequest)
        {
            var user = GetLoginUser(loginRequest.Username, loginRequest.Password);
            return LoginUser(user, loginRequest.Mandator);
        }

        #endregion

        /// <summary>
        /// Gets the login user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="UserSearchForLoginResult"/></returns>
        public UserSearchForLoginResult GetLoginUser(string username, string password)
        {
            var systemSession = new Session(Guid.Empty, null, new DateTime());
            var result = this.userRepository.FindForLogin(username, password, systemSession);
            if (result != null)
            {
                return result;
            }

            throw new InvalidLoginException(string.Format("The user '{0}' is not valid.", username));
        }

        private LoginResult LoginUser(UserSearchForLoginResult user, Mandator mandator)
        {
            //Check if user is marked as active.
            if (user.User.FlagActive == false)
            {
                log.Warn(string.Format("The user {0} couldn't login because it's not active.", user.User.Id));
                return new LoginResult(null, false);
            }

            if (mandator != null)
            {
                return CreateSessionLoginResult(user, mandator);
            }

            if (user.Mandators.Count() == 1)
            {
                return CreateSessionLoginResult(user, user.Mandators.First());
            }

            if (user.Mandators.Count() > 1)
            {
                return new LoginResult(null, false, true, user.Mandators);
            }

            throw new InvalidLoginException("Invalid login.");
        }


        private LoginResult CreateSessionLoginResult(UserSearchForLoginResult result, Mandator mandator)
        {
            //Clear mandators picture to avoid sending the logo in each service call :)
            mandator.Picture = null;
            var sessionData = new SessionData(result.User, mandator);
            return new LoginResult(sessionData, true);
        }
    }
}
