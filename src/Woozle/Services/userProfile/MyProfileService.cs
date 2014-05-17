using Woozle.Domain.PasswordChange;
using Woozle.Domain.UserProfile;
using Woozle.Services.Authentication;

namespace Woozle.Services.UserProfile
{
    [MandatorAuthenticate]
    public class MyProfileService : MandatorAuthenticatedService
    {
        private readonly IMyProfileLogic logic;
        private readonly IPasswordChangeLogic passwordChangeLogic;

        public MyProfileService(
            IMyProfileLogic logic,
            IPasswordChangeLogic passwordChangeLogic)
        {
            this.logic = logic;
            this.passwordChangeLogic = passwordChangeLogic;
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
            this.passwordChangeLogic.ChangePassword(changedProfileData.OldPassword, changedProfileData.NewPassword, Session.SessionData);
        }
    }
}
