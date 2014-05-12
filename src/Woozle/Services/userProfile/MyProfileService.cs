using Woozle.Domain.UserProfile;
using Woozle.Services.Authentication;

namespace Woozle.Services.UserProfile
{
    [MandatorAuthenticate]
    public class MyProfileService : AbstractService
    {
        private readonly IMyProfileLogic logic;

        public MyProfileService(IMyProfileLogic logic)
        {
            this.logic = logic;
        }

        /// <summary>
        /// Updates the given profile data
        /// </summary>
        /// <param name="changedProfileData"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public void Put(MyProfileData changedProfileData)
        {
            logic.Update(changedProfileData.Email, changedProfileData.LanguageId, Session.SessionData);
        }
        
        /// <summary>
        /// Changes the password of the logged in user acc. to the given new password
        /// </summary>
        /// <param name="changedProfileData"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public void Put(ChangeMyPasswordData changedProfileData)
        {
            logic.ChangePassword(changedProfileData.OldPassword, changedProfileData.NewPassword, Session.SessionData);
        }
    }
}
