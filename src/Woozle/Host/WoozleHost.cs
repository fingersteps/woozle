using System.Reflection;
using Funq;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Dependencies;
using Woozle.Domain.MandatorManagement;
using Woozle.Model.SessionHandling;
using Woozle.Persistence.Ef.Dependencies;
using Woozle.Services;
using Woozle.Services.Authentication;
using Woozle.Settings;

namespace Woozle.Host
{
    /// <summary>
    /// HTTP Serverhost based on <see cref="AppHostHttpListenerBase"/>
    /// </summary>
    public class WoozleHost : AppHostHttpListenerBase
    {
        private readonly string defaultMandatorName;

        /// <summary>
        /// Initializes a new <see cref="WoozleHost"/>
        /// </summary>
        /// <param name="defaultMandatorName">The default mandator name used in Woozle when no mandator is necessary (for example in public web services or in user registration)</param>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="assemblies">The assemblies which contain services</param>
        protected WoozleHost(string defaultMandatorName, string serviceName, params Assembly[] assemblies)
            : base(serviceName, assemblies)
        {
            this.defaultMandatorName = defaultMandatorName;
        }

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
            ConfigureDefaultScope(container);
            MappingConfiguration.Configure();
            Plugins.Add(new SessionFeature());
            Plugins.Add(CreateAuthFeature(container));
            Plugins.Add(new WoozleEntityFrameworkPlugin());
            Plugins.Add(new WoozlePlugin(defaultMandatorName));
        }

        /// <summary>
        /// Sets the default reuse scope for the IoC container, which takes effect for all default dependency injection bindings.
        /// </summary>
        /// <param name="container"></param>
        private static void ConfigureDefaultScope(Container container)
        {
            container.DefaultReuse = ReuseScope.Request;
        }

        protected virtual AuthFeature CreateAuthFeature(Container container)
        {
            var authFeature = new AuthFeature(() => new Session(), new IAuthProvider[]
            {
                new WoozleCredentialsAuthProvider(container)
            })
            {
                HtmlRedirect = string.Empty
            };
            return authFeature;
        }
    }
}
