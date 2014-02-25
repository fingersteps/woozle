using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.UserSearch;

namespace Woozle.Domain.Authentication
{
    /// <summary>
    /// Authentication businesslogic.
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
