using Woozle.Domain.Translation;
using Woozle.Model.SessionHandling;

namespace Woozle.Services
{
    public abstract class AbstractService : ServiceStack.Service
    {
        public ITranslator Translator { get; set; }

        protected new Session Session
        {
            get
            {
                var session = this.GetSession() as Session;
                return session;
            }
        }
    }
}
