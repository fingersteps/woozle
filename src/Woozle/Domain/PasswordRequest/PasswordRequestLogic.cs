using System;
using System.Diagnostics;
using Woozle.Domain.Communication;
using Woozle.Domain.ExternalSystem;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.UserManagement;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordRequestLogic : IPasswordRequestLogic
    {
        private readonly IUserLogic userLogic;
        private readonly IPasswordChangeLogic passwordChangeLogic;
        private readonly ICommunicationProvider communicationProvider;
        private readonly IPasswordGenerator passwordGenerator;

        public PasswordRequestLogic(
            IUserLogic userLogic, 
            IPasswordChangeLogic passwordChangeLogic,
            ICommunicationProvider communicationProvider,
            IPasswordGenerator passwordGenerator)
        {
            this.userLogic = userLogic;
            this.passwordChangeLogic = passwordChangeLogic;
            this.communicationProvider = communicationProvider;
            this.passwordGenerator = passwordGenerator;
        }

        public ExternalSystemCredentials Credentials
        {
            get { return this.communicationProvider.Credentials; }
            set { this.communicationProvider.Credentials = value; }
        }

        public bool RequestNewPassword(string username, string text, string title, SessionData sessionData,  
            Func<string, string, SessionData, string> getEmailText)
        {
            if (string.IsNullOrEmpty(username))
            {
                const string message = "The specified username is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException(message, "username");
            }

            var loadedUser = this.userLogic.GetUserByUsername(username, sessionData);

            if (loadedUser == null)
            {
                var message = string.Format("The user with the username {0} was not found.", username);
                Trace.TraceError(message);
                throw new ArgumentException(message, "username");
            }

            var newPassword = this.passwordGenerator.GetRandomPassword();

            this.passwordChangeLogic.ChangePassword(loadedUser, newPassword, sessionData);

            var resolvedText = getEmailText(text, newPassword, sessionData);

            return this.communicationProvider.Send(loadedUser.Email, title, resolvedText, sessionData);
        }
    }
}
