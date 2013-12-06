using Woozle.Core.Model.Authentication;
using Woozle.Core.Model.UserSearch;

namespace Woozle.Core.BusinessLogic.Authentication
{
    /// <summary>
    /// Interface for the authentication businesslogic.
    /// </summary>
    /// <remarks></remarks>
    public interface IAuthenticationLogic
    {

        /// <summary>
        /// Performs the login.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        LoginResult Login(LoginRequest loginRequest);

        /// <summary>
        /// Gets the login user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="UserSearchForLoginResult"/></returns>
        UserSearchForLoginResult GetLoginUser(string username, string password);
    }
}
