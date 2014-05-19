using System;
using Woozle.Domain.ExternalSystem;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordRequestLogic
    {
        /// <summary>
        /// Credentials for the external communication system (e.g. an email system)
        /// </summary>
        ExternalSystemCredentials Credentials { get; set; }

        /// <summary>
        /// Requests the new password.
        /// </summary>
        /// <param name="ipAdress">The ip of the callee.</param>
        /// <param name="username">The requested username</param>
        /// <param name="text">The text for sending the new password.</param>
        /// <param name="title">The title of the message for the new password.</param>
        /// <param name="sessionData"><see cref="SessionData"/></param>
        /// <param name="getEmailText">Callback for resolving parameters of the email text with the new password.</param>
        /// <returns></returns>
        bool RequestNewPassword(string ipAdress, string username, string text, string title, SessionData sessionData,
            Func<string, string, SessionData, string> getEmailText);
    }
}
