using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Model.SessionHandling;

namespace Woozle.Services
{
    /// <summary>
    /// Abstract service implementation
    /// </summary>
    public abstract class AbstractService : Service
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
