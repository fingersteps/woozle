using System.Diagnostics;
using Funq;
using ServiceStack.Common;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.Communication;
using Woozle.Domain.Fields;
using Woozle.Domain.Location;
using Woozle.Domain.MandatorManagement;
using Woozle.Domain.ModuleManagement;
using Woozle.Domain.Numerator;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.PasswordRequest;
using Woozle.Domain.PermissionManagement;
using Woozle.Domain.PersonManagement;
using Woozle.Domain.Settings;
using Woozle.Domain.StatusFields;
using Woozle.Domain.UserManagement;
using Woozle.Domain.UserProfile;
using Woozle.Host;
using Woozle.Settings;

namespace Woozle.Dependencies
{
    /// <summary>
    /// Register all dependencies for running Woozle (Core)
    /// </summary>
    public class WoozlePlugin : IPlugin
    {
        private readonly WoozleDefaults defaults;

        public WoozlePlugin(WoozleDefaults defaults)
        {
            this.defaults = defaults;
        }

        public void Register(IAppHost appHost)
        {
            Trace.TraceInformation("Register all dependencies.");
            var container = appHost.Config.ServiceManager.Container;
            container.Register<IHashProvider>(c => new SaltedHash());
            container.RegisterAs<PermissionsLogic, IPermissionProvider>();
            container.RegisterAs<PermissionManager, IPermissionManager>();
            container.RegisterAs<AuthenticationLogic, IAuthenticationLogic>();
            container.RegisterAs<ModuleLogic, IModuleLogic>();
            container.RegisterAs<UserValidator, IUserValidator>();
            container.RegisterAs<UserLogic, IUserLogic>();
            container.RegisterAs<MyProfileLogic, IMyProfileLogic>();
            container.RegisterAs<LocationLogic, ILocationLogic>();
            container.RegisterAs<GetRolesLogic, IGetRolesLogic>();
            container.RegisterAs<PermissionsLogic, IPermissionsLogic>();
            container.RegisterAs<PersonLogic, IPersonLogic>();
            container.RegisterAs<MandatorLogic, IMandatorLogic>();
            container.RegisterAs<SettingsLogic, ISettingsLogic>();
            container.RegisterAs<WoozleSettings, IWoozleSettings>()
                     .ReusedWithin(ReuseScope.Container);
            container.RegisterAs<StatusFieldLogic, IStatusFieldLogic>();
            container.RegisterAs<NumberProvider, INumberProvider>();
            container.RegisterAs<PasswordRequestLogic, IPasswordRequestLogic>();
            container.RegisterAs<PasswordChangeLogic, IPasswordChangeLogic>();
            container.RegisterAs<TextFieldLogic, ITextFieldLogic>();
            container.RegisterAs<PlaceHolderLogic, IPlaceholderLogic>();
            container.RegisterAs<PasswordGenerator, IPasswordGenerator>();
            container.RegisterAs<EmailCommunicationProvider, ICommunicationProvider>();

            ConfigureDefaultMandator(container);
        }


        /// <summary>
        /// The configured default mandator and languagewill be used as default for all public services (where no authentification is needed).
        /// </summary>
        private void ConfigureDefaultMandator(Container container)
        {
            Trace.TraceInformation("Configure Woozle defaults.");
            if (defaults != null)
            {
                var woozleSettings = container.Resolve<IWoozleSettings>();
                if (!defaults.DefaultMandatorName.IsNullOrEmpty())
                {
                    var defaultMandator = container.Resolve<IMandatorLogic>().LoadMandator(defaults.DefaultMandatorName);
                    woozleSettings.DefaultMandator = defaultMandator;
                }
                if (!defaults.DefaultLanguageCode.IsNullOrEmpty())
                {
                    var defaultLanguage = container.Resolve<ILocationLogic>().LoadLanguage(defaults.DefaultLanguageCode);
                    woozleSettings.DefaultLanguage = defaultLanguage;
                }
            }
        }
    }
}
