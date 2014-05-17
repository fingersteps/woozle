using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Model.SessionHandling;
using Woozle.Settings;

namespace Woozle.Services
{
    /// <summary>
    /// Abstract service implementation for all services which can be used by successful authenticated Woozle users
    /// </summary>
    public abstract class MandatorAuthenticatedService : Service
    {
        /// <summary>
        /// <see cref="Session">Session </see> for authorisation
        /// </summary>
        protected new Session Session
        {
            get
            {
                var httpRequest = this.RequestContext.Get<IHttpRequest>();
                var session = httpRequest.GetSession() as Session;
                return session;
            }
        }
    }
}
