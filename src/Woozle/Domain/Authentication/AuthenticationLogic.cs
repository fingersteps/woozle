using System;
using System.Linq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Persistence;
using Woozle.Persistence.Ef;


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
        /// <see cref="IUnitOfWork"/>
        /// </summary>
        private readonly IEfUnitOfWork unitOfWork;

        private readonly IHashProvider passwordHasher;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="userRepository"><see cref="IUserRepository"/></param>
        /// <param name="unitOfWork"><see cref="IUnitOfWork"/></param>
        /// <param name="passwordHasher"></param>
        public AuthenticationLogic(
            IUserRepository userRepository,
            IEfUnitOfWork unitOfWork,
            IHashProvider passwordHasher)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
        }

        #region IAuthenticationLogic Members

        /// <summary>
        /// Performs the login with the given request information.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public LoginResult Login(LoginRequest loginRequest)
        {
            var userAndMandators = GetLoginUser(loginRequest.Username);
            ValidatePassword(userAndMandators.User, loginRequest.Password);
            return LoginUser(userAndMandators, loginRequest.Mandator);
        }

        private void ValidatePassword(User user, string password)
        {
            bool validPassword = false;
            if (user.PasswordHash != null && user.PasswordSalt != null)
            {
                validPassword = passwordHasher.VerifyHashString(password, user.PasswordHash, user.PasswordSalt);
            }
            
            if (!validPassword)
            {
                throw new InvalidLoginException("Invalid login.");
            }
        }

        #endregion

        /// <summary>
        /// Gets the login user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The <see cref="UserSearchForLoginResult"/></returns>
        public UserSearchForLoginResult GetLoginUser(string username)
        {
            var result = this.userRepository.FindForLogin(username);
            if (result != null)
            {
                return result;
            }

            throw new InvalidLoginException(string.Format("The user '{0}' is not valid.", username));
        }

        private LoginResult LoginUser(UserSearchForLoginResult user, Mandator mandator)
        {
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
            var user = result.User;
            var sessionData = new SessionData(user, mandator);
            this.UpdateLastLogin(user, sessionData);
            return new LoginResult(sessionData, true);
        }

        private void UpdateLastLogin(User user, SessionData sessionData)
        {
            user.LastLogin = DateTime.Now;
            this.userRepository.Save(user, sessionData); 
            this.unitOfWork.Commit();
        }
    }
}
