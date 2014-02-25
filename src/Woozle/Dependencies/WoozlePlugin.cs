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
using Woozle.Services.Authentication;
using Woozle.Settings;

namespace Woozle.Dependencies
{
    /// <summary>
    /// Register all dependencies for running Woozle (Core)
    /// </summary>
    public class WoozlePlugin : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            var container = appHost.Config.ServiceManager.Container;
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
        }
    }
}
