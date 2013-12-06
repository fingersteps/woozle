using ServiceStack;
using ServiceStack.Web;
using Woozle.Core.Common.Translate;
using Woozle.Core.Model.SessionHandling;

namespace Woozle.Core.Services.Stack.Impl
{
    public abstract class AbstractService : Service
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
