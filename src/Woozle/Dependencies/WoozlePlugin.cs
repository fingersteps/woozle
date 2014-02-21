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
using Woozle.Domain.UserManagement;
using Woozle.Services.Authentication;

namespace Woozle.Dependencies
{
    /// <summary>
    /// Register all dependencies for running Woozle (Core)
    /// </summary>
    public class WoozlePlugin : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.RegisterAs<PermissionsLogic, IPermissionProvider>();
            appHost.RegisterAs<PermissionManager, IPermissionManager>();
            appHost.RegisterAs<AuthenticationLogic, IAuthenticationLogic>();
            appHost.RegisterAs<ModuleLogic, IModuleLogic>();
            appHost.RegisterAs<UserLogic, IUserLogic>();
            appHost.RegisterAs<UserBusinessValidator, IUserValidator>();
            appHost.RegisterAs<LocationLogic, ILocationLogic>();
            appHost.RegisterAs<GetRolesLogic, IGetRolesLogic>();
            appHost.RegisterAs<PermissionsLogic, IPermissionsLogic>();
            appHost.RegisterAs<PersonLogic, IPersonLogic>();
            appHost.RegisterAs<MandatorLogic, IMandatorLogic>();
            appHost.RegisterAs<SettingsLogic, ISettingsLogic>();
            appHost.RegisterAs<WoozleAuthRepository, IUserAuthRepository>();
        }
    }
}
