using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Translation;
using Woozle.Model.SessionHandling;

namespace Woozle.Services
{
    public abstract class AbstractService : ServiceStack.ServiceInterface.Service
    {
        public ITranslator Translator { get; set; }

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
