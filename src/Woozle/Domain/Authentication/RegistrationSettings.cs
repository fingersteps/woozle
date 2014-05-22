using Woozle.Model;

namespace Woozle.Domain.Authentication
{
    public class RegistrationSettings : IRegistrationSettings
    {
        public Status DefaultFlagActiveStatus { get; set; }
    }
}
