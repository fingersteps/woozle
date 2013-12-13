using System.Reflection;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Dependencies;
using Woozle.Domain.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;

namespace Woozle.Host
{
    public class WoozleHost : AppHostHttpListenerBase
    {
        protected WoozleHost(string serviceName, params Assembly[] assemblies) : base(serviceName, assemblies)
        {
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new SessionFeature());

            //Configure Authentication
            Plugins.Add(new AuthFeature(() => new Session(), new IAuthProvider[]
                                                                 {
                                                                     new WoozleCredentialsAuthProvider(container)
                                                                 })
            {
                HtmlRedirect = string.Empty
            });

            Plugins.Add(new WoozlePlugin());
        }
    }
}
