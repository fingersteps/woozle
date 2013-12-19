using System.Reflection;
using Funq;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Dependencies;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;

namespace Woozle.Host
{
    /// <summary>
    /// HTTP Serverhost based on <see cref="AppHostHttpListenerBase"/>
    /// </summary>
    public class WoozleHost : AppHostHttpListenerBase
    {
      
        /// <summary>
        /// Initializes a new <see cref="WoozleHost"/>
        /// </summary>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="assemblies">The assemblies which contain services</param>
        protected WoozleHost(string serviceName, params Assembly[] assemblies) : base(serviceName, assemblies)
        {
        }

        /// <summary>
        /// Configures the host.
        /// </summary>
        /// <remarks>
        ///  - Adds the <see cref="SessionFeature"/>
        ///  - Adds the <see cref="AuthFeature"/> with the <see cref="WoozleCredentialsAuthProvider"/>
        ///  - Adds the <see cref="WoozlePlugin"/>
        /// </remarks>
        /// <param name="container">The IoC-Container</param>
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
