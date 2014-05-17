using System;
using System.Diagnostics;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordChange
{
    public class PasswordChangeLogic : IPasswordChangeLogic
    {
        private readonly IHashProvider hashProvider;
        private readonly IUserValidator userValidator;
        private readonly IUserLogic userLogic;

        public PasswordChangeLogic(
            IHashProvider hashProvider,
            IUserValidator userValidator,
            IUserLogic userLogic)
        {
            this.hashProvider = hashProvider;
            this.userValidator = userValidator;
            this.userLogic = userLogic;
        }

        public bool ChangePassword(User user, string newPassword, SessionData sessionData)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                const string message = "The new password is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException("newPassword", message);
            }

            if (user == null)
            {
                Trace.TraceError("The user is null.");
                throw new ArgumentNullException("user");
            }

            var loadedUser = this.userLogic.LoadUser(user.Id, sessionData);

            if (loadedUser != null)
            {
                Trace.TraceInformation("Change password of user '{0}'", user.Username);
                string newHash;
                string newSalt;
                hashProvider.GetHashAndSaltString(newPassword, out newHash, out newSalt);
                user.PasswordHash = newHash;
                user.PasswordSalt = newSalt;
                userLogic.Save(user, sessionData);

                return true;
            }

            Trace.TraceError("Could not found user with the id '{0}'.", user.Id);

            return false;
        }

        public bool ChangePassword(string oldPassword, string newPassword, SessionData sessionData)
        {
            var user = sessionData.User;
            ValidateOldPassword(user, oldPassword, sessionData);
            userValidator.ValidateUserPassword(newPassword);

            string newHash;
            string newSalt;
            hashProvider.GetHashAndSaltString(newPassword, out newHash, out newSalt);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            userLogic.Save(user, sessionData);

            return true;
        }

        private void ValidateOldPassword(User user, string oldPassword, SessionData sessionData)
        {
            if (!hashProvider.VerifyHashString(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("The given old password is wrong.");
            }
        }

    }
}
