using Woozle.Domain.ExternalSystem;

namespace Woozle.ExternalSystem
{
    /// <summary>
    /// Definition for an external system which is able to perform EMail related stuff.
    /// </summary>
    public interface IExternalEMailSystem : IExternalSystem
    {
        /// <summary>
        /// Credentials for the specific email system.
        /// </summary>
        ExternalEmailSystemCredentials Credentials { get; }

        /// <summary>
        /// Sends an EMail to a desired destination address
        /// </summary>
        /// <param name="fromName"> </param>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"> </param>
        /// <param name="text"></param>
        bool SendEMail(string fromName, string fromAddress, string toAddress, string subject, string text);
    }
}
