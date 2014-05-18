using System;
using System.Diagnostics;
using Woozle.Domain.ExternalSystem;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.ExternalSystem.Mail;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Communication
{
    public class EmailCommunicationProvider : ICommunicationProvider
    {
        private readonly IExternalSystemFacadeFactory externalSystemFacadeFactory;

        public EmailCommunicationProvider(IExternalSystemFacadeFactory externalSystemFacadeFactory)
        {
            this.externalSystemFacadeFactory = externalSystemFacadeFactory;
        }

        public ExternalSystemCredentials Credentials { get; set; }

        public bool Send(string adress, string title, string text, SessionData sessionData)
        {
            var mailSystem = this.GetMailSystem(sessionData);

            if (mailSystem == null)
            {
                const string message = "E-Mail System couldn't found.";
                Trace.TraceError(message);
                throw new SystemException(message);
            }

            mailSystem.Credentials = this.Credentials;
            return mailSystem.SendEMail(sessionData.Mandator.Name, sessionData.Mandator.Email, adress, title, text);
        }

        private IExternalMailSystem GetMailSystem(SessionData sessionData)
        {
            var facade = this.externalSystemFacadeFactory.GetExternalSystemFacade<IExternalMailSystem>();
            return facade.GetExternalSystem(sessionData);
        }
    }
}
