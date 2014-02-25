using Woozle.Model;

namespace Woozle.Domain.Authentication
{
    public class RegistrationSettings : IRegistrationSettings
    {
        public Language DefaultLanguage { get; set; }
        public Status DefaultFlagActiveStatus { get; set; }
    }
}
