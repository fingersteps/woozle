using Funq;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using Woozle.Domain.Authentication;
using Woozle.Domain.Location;
using Woozle.Domain.StatusFields;

namespace Woozle.Services.Authentication
{
    /// <summary>
    /// This Feature is a Woozle specific extension of ServiceStacks built in Registration feature.
    /// </summary>
    public class WoozleRegistrationFeature : IPlugin
    {
        private readonly string defaultLanguageCode;
        private readonly bool registeredUsersAreActiveImmediately;
        public string AtRestPath { get; set; }

        public WoozleRegistrationFeature() : this("en", true)
        {
        }

        public WoozleRegistrationFeature(string defaultLanguageCode, bool registeredUsersAreActiveImmediately)
        {
            this.defaultLanguageCode = defaultLanguageCode;
            this.registeredUsersAreActiveImmediately = registeredUsersAreActiveImmediately;
            this.AtRestPath = "/register";
        }


        public void Register(IAppHost appHost)
        {
            appHost.RegisterService<RegistrationService>(AtRestPath);
            appHost.RegisterAs<WoozleAuthRepository, IUserAuthRepository>();
            appHost.RegisterAs<RegistrationValidator, IValidator<Registration>>();
            appHost.Config.ServiceManager.Container.RegisterAs<RegistrationSettings, IRegistrationSettings>().ReusedWithin(ReuseScope.Container);
            ConfigureWoozleRegistration(appHost);
        }

        private void ConfigureWoozleRegistration(IAppHost appHost)
        {
            var registrationSettings = appHost.TryResolve<IRegistrationSettings>();

            registrationSettings.DefaultLanguage = appHost.TryResolve<ILocationLogic>().LoadLanguage(defaultLanguageCode);

            var flagActiveStatusValue = registeredUsersAreActiveImmediately ? "Active" : "Inactive";
            registrationSettings.DefaultFlagActiveStatus = appHost.TryResolve<IStatusFieldLogic>().LoadStatusByValue(flagActiveStatusValue);
        }
    }
}
