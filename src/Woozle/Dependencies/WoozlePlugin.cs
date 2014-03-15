using Funq;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.Location;
using Woozle.Domain.MandatorManagement;
using Woozle.Domain.ModuleManagement;
using Woozle.Domain.PermissionManagement;
using Woozle.Domain.PersonManagement;
using Woozle.Domain.Settings;
using Woozle.Domain.StatusFields;
using Woozle.Domain.UserManagement;
using Woozle.Persistence;
using Woozle.Settings;

namespace Woozle.Dependencies
{
    /// <summary>
    /// Register all dependencies for running Woozle (Core)
    /// </summary>
    public class WoozlePlugin : IPlugin
    {
        private readonly string defaultMandatorName;

        public WoozlePlugin(string defaultMandatorName)
        {
            this.defaultMandatorName = defaultMandatorName;
        }

        public void Register(IAppHost appHost)
        {
            var container = appHost.Config.ServiceManager.Container;
            container.Register<IHashProvider>(c => new SaltedHash());
            container.RegisterAs<PermissionsLogic, IPermissionProvider>();
            container.RegisterAs<PermissionManager, IPermissionManager>();
            container.RegisterAs<AuthenticationLogic, IAuthenticationLogic>();
            container.RegisterAs<ModuleLogic, IModuleLogic>();
            container.RegisterAs<UserLogic, IUserLogic>();
            container.RegisterAs<LocationLogic, ILocationLogic>();
            container.RegisterAs<GetRolesLogic, IGetRolesLogic>();
            container.RegisterAs<PermissionsLogic, IPermissionsLogic>();
            container.RegisterAs<PersonLogic, IPersonLogic>();
            container.RegisterAs<MandatorLogic, IMandatorLogic>();
            container.RegisterAs<SettingsLogic, ISettingsLogic>();
            container.RegisterAs<WoozleSettings, IWoozleSettings>()
                .ReusedWithin(ReuseScope.Container);
            container.RegisterAs<StatusFieldLogic, IStatusFieldLogic>();

            ConfigureDefaultMandator(container);
        }


        /// <summary>
        /// The configured default mandator will be used as default for all public services (where no authentification is needed) and for user registration if it's set.
        /// </summary>
        private void ConfigureDefaultMandator(Container container)
        {
            if (!string.IsNullOrEmpty(defaultMandatorName))
            {
                var defaultMandator = container.Resolve<IMandatorLogic>().LoadMandator(defaultMandatorName);
                container.Resolve<IWoozleSettings>().DefaultMandator = defaultMandator;
            }
        }
    }
}
