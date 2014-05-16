using Woozle.Domain.ExternalSystem.Mail;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestLogic
    {
        ExternalMailSystemCredentials Credentials { get; set; }
        bool RequestNewPassword(string username, string title, string text, SessionData sessionData);
    }
}
