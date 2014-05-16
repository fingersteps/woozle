using Woozle.Model.SessionHandling;

namespace Woozle.Domain.UserProfile
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
    }
}
