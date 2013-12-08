using Funq;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.Location;
using Woozle.Domain.MandatorManagement;
using Woozle.Domain.ModuleManagement;
using Woozle.Domain.PermissionManagement;
using Woozle.Domain.PersonManagement;
using Woozle.Domain.Settings;
using Woozle.Domain.UserManagement;

namespace Woozle.Dependencies
{
    public class WoozleDomainDependencies : IWoozleDependency
    {
        public void Register(Container container)
        {
            container.RegisterAutoWiredAs<PermissionsLogic, IPermissionProvider>();
            container.RegisterAutoWiredAs<PermissionManager, IPermissionManager>();
            container.RegisterAutoWiredAs<AuthenticationLogic, IAuthenticationLogic>();
            container.RegisterAutoWiredAs<ModuleLogic, IModuleLogic>();
            container.RegisterAutoWiredAs<UserLogic, IUserLogic>();
            container.RegisterAutoWiredAs<UserBusinessValidator, IUserValidator>();
            container.RegisterAutoWiredAs<LocationLogic, ILocationLogic>();
            container.RegisterAutoWiredAs<GetRolesLogic, IGetRolesLogic>();
            container.RegisterAutoWiredAs<PermissionsLogic, IPermissionsLogic>();
            container.RegisterAutoWiredAs<PersonLogic, IPersonLogic>();
            container.RegisterAutoWiredAs<MandatorLogic, IMandatorLogic>();
            container.RegisterAutoWiredAs<SettingsLogic, ISettingsLogic>();
        }
    }
}
