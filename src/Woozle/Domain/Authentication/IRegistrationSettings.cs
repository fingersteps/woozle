using Woozle.Model;

namespace Woozle.Domain.Authentication
{
    public interface IRegistrationSettings
    {
        Language DefaultLanguage { get; set; }
        Status DefaultFlagActiveStatus { get; set; }
    }
}
