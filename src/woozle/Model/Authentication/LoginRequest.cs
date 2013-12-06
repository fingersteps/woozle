using Woozle.Model;

namespace Woozle.Core.Model.Authentication
{
    /// <summary>
    /// Represents a login request
    /// </summary>
    /// <remarks></remarks>
    public class LoginRequest
    {
        #region ILoginRequest Members

        /// <summary>
        /// Gets or sets the mandator.
        /// </summary>
        /// <value>The mandator.</value>
        /// <remarks></remarks>
        public Mandator Mandator { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        /// <remarks></remarks>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// <remarks></remarks>
        public string Password { get; set; }

        #endregion
    }
}
