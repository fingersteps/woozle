using Woozle.Model;

namespace Woozle.Domain.Authentication
{
    public interface IRegistrationSettings
    {
        Status DefaultFlagActiveStatus { get; set; }
    }
}
