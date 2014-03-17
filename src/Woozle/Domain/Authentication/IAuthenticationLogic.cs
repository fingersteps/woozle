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
        /// Logins the already authenticated userd to a specific mandator
        /// </summary>
        /// <param name="username"></param>
        /// <param name="mandator"></param>
        /// <returns></returns>
        LoginResult LoginMandator(string username, Mandator mandator);

        /// <summary>
        /// Gets the login user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The <see cref="UserSearchForLoginResult"/></returns>
        UserSearchForLoginResult GetLoginUser(string username);
    }
}
