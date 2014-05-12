using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestLogic
    {
        bool RequestNewPassword(string username, SessionData sessionData);
    }
}
