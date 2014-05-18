using System;
using Woozle.Domain.ExternalSystem;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestLogic
    {
        ExternalSystemCredentials Credentials { get; set; }

        bool RequestNewPassword(string username, string text, string title, SessionData sessionData,
            Func<string, string, SessionData, string> getEmailText);
    }
}
