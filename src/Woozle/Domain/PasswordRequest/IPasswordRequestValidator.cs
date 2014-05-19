using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestValidator
    {
        /// <summary>
        /// Checks wheter the callee can request the new password.
        /// </summary>
        /// <param name="ip">The ip of the callee.</param>
        /// <param name="sessionData">The <see cref="SessionData"/></param>
        /// <returns>True, if the calle can request for a new password.</returns>
        bool CanRequestPassword(string ip, SessionData sessionData);
    }
}
