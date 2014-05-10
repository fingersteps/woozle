using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.userProfile
{
    public interface IMyProfileLogic
    {
        /// <summary>
        /// Updates the users profile acc. to the given arguments
        /// </summary>
        /// <param name="email"></param>
        /// <param name="languageId"></param>
        /// <param name="sessionData"></param>
        void Update(string email, int languageId, SessionData sessionData);

        /// <summary>
        /// Changes the password of the logged in user acc. to the given parameters.
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="sessionData"></param>
        User ChangePassword(string oldPassword, string newPassword, SessionData sessionData);
    }
}
