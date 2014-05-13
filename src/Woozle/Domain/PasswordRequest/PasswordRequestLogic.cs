using System;
using System.Diagnostics;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.UserManagement;
using Woozle.ExternalSystem;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordRequestLogic : IPasswordRequestLogic
    {
        private readonly IUserLogic userLogic;
        private readonly IExternalSystemFacadeFactory externalSystemFacadeFactory;

        public PasswordRequestLogic(IUserLogic userLogic, IExternalSystemFacadeFactory externalSystemFacadeFactory)
        {
            this.userLogic = userLogic;
            this.externalSystemFacadeFactory = externalSystemFacadeFactory;
        }

        public bool RequestNewPassword(string username, SessionData sessionData)
        {
            if (string.IsNullOrEmpty(username))
            {
                const string message = "The specified username is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException(message, "username");
            }

            var user = this.GetEmail(username, sessionData);

            var mailSystem = this.GetMailSystem(sessionData);

            if (mailSystem == null)
            {
                const string message = "E-Mail System couldn't found.";
                Trace.TraceError(message);
                throw new SystemException(message);
            }

            return this.SendMail(mailSystem, user, sessionData);
        }

        private bool SendMail(IExternalEMailSystem mailSystem, string userEmail, SessionData sessionData)
        {
            mailSystem.SendEMail(
                sessionData.Mandator.Name,
                sessionData.Mandator.Email,
                userEmail,
                "Passwort Request",
                "Your Password was set."
                );



            return true;
        }

        private IExternalEMailSystem GetMailSystem(SessionData sessionData)
        {
            var facade =  this.externalSystemFacadeFactory.GetExternalSystemFacade<IExternalEMailSystem>();
            return facade.GetExternalSystem(sessionData);
        }

        private string GetEmail(string username, SessionData sessionData)
        {
            var user = this.userLogic.GetUserByUsername(username, sessionData);

            if (user == null)
            {
                throw new ArgumentException("The user doesn't exists.", "username");
            }

            return user.Email;
        }
    }
}
