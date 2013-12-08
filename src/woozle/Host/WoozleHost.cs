using System.Reflection;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using Woozle.Dependencies;
using Woozle.Domain.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;
using Woozle.Services.Authority;
using Woozle.Services.Location;
using Woozle.Services.Mandator;
using Woozle.Services.Modules;
using Woozle.Services.Settings;
using Woozle.Services.UserManagement;

namespace Woozle.Host
{
    public class WoozleHost : AppHostHttpListenerBase
    {
        protected WoozleHost(string serviceName, params Assembly[] assemblies) : base(serviceName, assemblies)
        {
            RegisterCoreService();
        }

        private void RegisterCoreService()
        {
            RegisterService(typeof (MandatorRoleService));
            RegisterService(typeof(PermissionService));
            RegisterService(typeof(RoleService));
            RegisterService(typeof(LoginContextService));
            RegisterService(typeof(MandatorService));
            RegisterService(typeof(ModuleService));
            RegisterService(typeof(SettingService));
            RegisterService(typeof(LanguageService));
            RegisterService(typeof(UserService));
        }

        public override void Configure(Container container)
        {
            ConfigureDependencies(container);

            Plugins.Add(new SessionFeature());

            //Configure Authentication
            Plugins.Add(new AuthFeature(() => new Session(), new IAuthProvider[]
                                                                 {
                                                                     new WoozleCredentialsAuthProvider(Container.TryResolve<IAuthenticationLogic>())
                                                                 })
            {
                HtmlRedirect = string.Empty
            });
        }

        private void ConfigureDependencies(Container container)
        {
            container.Register<ICacheClient>(new MemoryCacheClient());
            new WoozleDomainDependencies().Register(container);
            new WoozleRepositoryDependencies().Register(container);
            new WoozlePersistenceDependencies().Register(container);
        }
    }
}
