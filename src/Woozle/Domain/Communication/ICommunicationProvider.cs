using Woozle.Domain.ExternalSystem;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Communication
{
    public interface ICommunicationProvider
    {
        ExternalSystemCredentials Credentials { get; set; }
        bool Send(string adress, string title, string text, SessionData sessionData);
    }
}
