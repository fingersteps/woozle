using System;
using System.Diagnostics;
using ServiceStack.ServiceHost;
using Woozle.Domain.Communication;
using Woozle.Domain.ExternalSystem;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordRequestLogic : IPasswordRequestLogic
    {
        private readonly IUserLogic userLogic;
        private readonly IPasswordChangeLogic passwordChangeLogic;
        private readonly ICommunicationProvider communicationProvider;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly IRepository<Model.PasswordRequest> passwordRequestRepository;
        private readonly IPasswordRequestValidator passwordRequestValidator;
        private readonly IRequestContext requestContext;

        public PasswordRequestLogic(
            IUserLogic userLogic, 
            IPasswordChangeLogic passwordChangeLogic,
            ICommunicationProvider communicationProvider,
            IPasswordGenerator passwordGenerator,
            IRepository<Model.PasswordRequest> passwordRequestRepository,
            IPasswordRequestValidator passwordRequestValidator)
        {
            this.userLogic = userLogic;
            this.passwordChangeLogic = passwordChangeLogic;
            this.communicationProvider = communicationProvider;
            this.passwordGenerator = passwordGenerator;
            this.passwordRequestRepository = passwordRequestRepository;
            this.passwordRequestValidator = passwordRequestValidator;
            //this.requestContext = requestContext;
        }

        public ExternalSystemCredentials Credentials
        {
            get { return this.communicationProvider.Credentials; }
            set { this.communicationProvider.Credentials = value; }
        }

        public bool RequestNewPassword(string ipAdress, string username, string text, string title, SessionData sessionData,  
            Func<string, string, SessionData, string> getEmailText)
        {
            if (string.IsNullOrEmpty(username))
            {
                const string message = "The specified username is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException(message, "username");
            }

            if (!this.passwordRequestValidator.CanRequestPassword(ipAdress, sessionData))
            {
                return false;
            }

            this.passwordRequestRepository.Save(new Model.PasswordRequest
                                                {
                                                    IP = ipAdress,
                                                    PersistanceState = PState.Added,
                                                    TimeStamp = DateTime.Now,
                                                    Username = username
                                                }, sessionData);

            this.passwordRequestRepository.UnitOfWork.Commit();
            
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
