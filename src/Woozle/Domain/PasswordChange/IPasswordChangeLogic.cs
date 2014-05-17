using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordChange
{
    public interface IPasswordChangeLogic
    {
        /// <summary>
        /// Changes the password of a specific user.
        /// </summary>
        /// <param name="user">The specific user.</param>
        /// <param name="newPassword">New password</param>
        /// <param name="sessionData">The current <see cref="SessionData">Session</see></param>
        /// <returns>true, if the password was successfully changed.</returns>
        bool ChangePassword(User user, string newPassword, SessionData sessionData);

        /// <summary>
        /// Changes the password of the current logged on user.
        /// </summary>
        /// <param name="newPassword">The new passsword.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="sessionData">The current <see cref="SessionData">Session</see></param>
        /// <returns>true, if the password was successfully changed.</returns>
        bool ChangePassword(string oldPassword, string newPassword, SessionData sessionData);
    }
}
