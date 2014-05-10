using Woozle.Domain.UserManagement;
using Woozle.Services.Authentication;

namespace Woozle.Services.userProfile
{
    [MandatorAuthenticate]
    public class MyProfileService : AbstractService
    {
        private readonly IUserLogic logic;

        public MyProfileService(IUserLogic logic)
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
            var user = Session.SessionData.User;
            user.Email = changedProfileData.Email;
            user.LanguageId = changedProfileData.LanguageId;
            logic.Save(user, Session.SessionData);
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
