using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Model.SessionHandling;
using Woozle.Settings;

namespace Woozle.Services
{
    /// <summary>
    /// An abstract service implementation for public services. Provides global functionality which can be used in all public services
    /// A public service can be used without the authentication of the user
    /// </summary>
    public abstract class PublicService : Service
    {
        private readonly IWoozleSettings woozleSettings;

        protected PublicService(IWoozleSettings woozleSettings)
        {
            this.woozleSettings = woozleSettings;
        }

        /// <summary>
        /// <see cref="Session">Session </see> for authorisation
        /// </summary>
        protected new Session Session
        {
            get
            {
                var httpRequest = this.RequestContext.Get<IHttpRequest>();
                var session = httpRequest.GetSession() as Session;
                if (session != null && session.SessionData != null && session.SessionData.Mandator.Id == 0)
                {
                    session.SessionData.Mandator = woozleSettings.DefaultMandator;
                }
                return session;
            }
        }
    }
}
