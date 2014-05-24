using Funq;
using ServiceStack.FluentValidation;
using ServiceStack.WebHost.Endpoints;
using Woozle.Domain.Authentication;
using Woozle.Domain.Registration;
using Woozle.Domain.StatusFields;

namespace Woozle.Services.Registration
{
    /// <summary>
    /// This Feature is a Woozle specific extension of ServiceStacks built in Registration feature.
    /// </summary>
    public class WoozleRegistrationFeature : IPlugin
    {
        private readonly bool registeredUsersAreActiveImmediately;
        public string AtRestPath { get; set; }

        public WoozleRegistrationFeature() : this(true)
        {
        }

        public WoozleRegistrationFeature(bool registeredUsersAreActiveImmediately)
        {
            this.registeredUsersAreActiveImmediately = registeredUsersAreActiveImmediately;
            this.AtRestPath = "/register";
        }

        public void Register(IAppHost appHost)
        {
            appHost.RegisterService<RegistrationService>(AtRestPath);
            appHost.RegisterAs<RegistrationLogic, IRegistrationLogic>();
            appHost.Config.ServiceManager.Container.RegisterAs<RegistrationSettings, IRegistrationSettings>().ReusedWithin(ReuseScope.Container);
            ConfigureWoozleRegistration(appHost);
        }

        private void ConfigureWoozleRegistration(IAppHost appHost)
        {
            var registrationSettings = appHost.TryResolve<IRegistrationSettings>();
            var flagActiveStatusValue = registeredUsersAreActiveImmediately ? "Active" : "Inactive";
            registrationSettings.DefaultFlagActiveStatus = appHost.TryResolve<IStatusFieldLogic>().LoadStatusByValue(flagActiveStatusValue);
        }
    }
}
