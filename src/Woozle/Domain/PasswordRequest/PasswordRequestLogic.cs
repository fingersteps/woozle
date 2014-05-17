using System;
using System.Diagnostics;
using System.Linq;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.ExternalSystem.Mail;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.UserManagement;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordRequestLogic : IPasswordRequestLogic
    {
        private readonly IUserLogic userLogic;
        private readonly IExternalSystemFacadeFactory externalSystemFacadeFactory;
        private readonly IPasswordChangeLogic passwordChangeLogic;

        public PasswordRequestLogic(
            IUserLogic userLogic, 
            IExternalSystemFacadeFactory externalSystemFacadeFactory,
            IPasswordChangeLogic passwordChangeLogic)
        {
            this.userLogic = userLogic;
            this.externalSystemFacadeFactory = externalSystemFacadeFactory;
            this.passwordChangeLogic = passwordChangeLogic;
        }

        public ExternalMailSystemCredentials Credentials { get; set; }

        public bool RequestNewPassword(string username, string title, string text, SessionData sessionData)
        {
            if (string.IsNullOrEmpty(username))
            {
                const string message = "The specified username is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException(message, "username");
            }

            if (string.IsNullOrEmpty(text))
            {
                const string message = "The specified text is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException(message, "text");
            }


            var loadedUser = this.userLogic.GetUserByUsername(username, sessionData);

            this.passwordChangeLogic.ChangePassword(loadedUser, this.GetRandomPassword(), sessionData);

            var mailSystem = this.GetMailSystem(sessionData);

            if (mailSystem == null)
            {
                const string message = "E-Mail System couldn't found.";
                Trace.TraceError(message);
                throw new SystemException(message);
            }

            mailSystem.Credentials = this.Credentials;

            return this.SendMail(mailSystem, loadedUser.Email, title, text, sessionData);
        }

        private string GetRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
        }

        private bool SendMail(IExternalMailSystem mailSystem, string userEmail, string title, string text, SessionData sessionData)
        {
            return mailSystem.SendEMail(sessionData.Mandator.Name, sessionData.Mandator.Email, userEmail, title, text);
        }

        private IExternalMailSystem GetMailSystem(SessionData sessionData)
        {
            var facade =  this.externalSystemFacadeFactory.GetExternalSystemFacade<IExternalMailSystem>();
            return facade.GetExternalSystem(sessionData);
        }
    }
}
