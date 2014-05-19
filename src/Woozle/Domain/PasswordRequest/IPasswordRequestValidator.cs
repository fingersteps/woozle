using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestValidator
    {
        bool CanRequestPassword(string ip, SessionData sessionData);
    }
}
