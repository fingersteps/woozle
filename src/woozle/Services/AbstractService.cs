using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Model.SessionHandling;

namespace Woozle.Services
{
    /// <summary>
    /// Base Service
    /// </summary>
    public abstract class AbstractService : ServiceStack.ServiceInterface.Service
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
