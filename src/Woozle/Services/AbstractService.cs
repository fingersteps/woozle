using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using Woozle.Model.SessionHandling;
using Woozle.Settings;

namespace Woozle.Services
{
    /// <summary>
    /// Base Service
    /// </summary>
    public abstract class AbstractService : ServiceStack.ServiceInterface.Service
    {
       // private readonly IWoozleSettings woozleSettings;

       // protected AbstractService(IWoozleSettings woozleSettings)
        //{
        //    this.woozleSettings = woozleSettings;
       // }

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
              //      session.SessionData.Mandator = woozleSettings.DefaultMandator;
                }
                return session;
            }
        }
    }
}
