using Funq;
using Woozle.Core.BusinessLogic.Authentication;
using Woozle.Core.BusinessLogic.Authority;
using Woozle.Core.BusinessLogic.Cities;
using Woozle.Core.BusinessLogic.Impl.Authentication;
using Woozle.Core.BusinessLogic.Impl.Authority;
using Woozle.Core.BusinessLogic.Impl.Cities;
using Woozle.Core.BusinessLogic.Impl.MandatorManagement;
using Woozle.Core.BusinessLogic.Impl.ModuleManagement;
using Woozle.Core.BusinessLogic.Impl.PersonManagement;
using Woozle.Core.BusinessLogic.Impl.Settings;
using Woozle.Core.BusinessLogic.Impl.UserManagement;
using Woozle.Core.BusinessLogic.MandatorManagement;
using Woozle.Core.BusinessLogic.ModuleManagement;
using Woozle.Core.BusinessLogic.PermissionManagement;
using Woozle.Core.BusinessLogic.Settings;
using Woozle.Core.BusinessLogic.UserManagement;
using Woozle.Core.Common.PermissionManagement.Impl;

namespace Woozle.Core.Dependencies
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
