using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence
{
    public interface IExternalSystemRepository
    {
        ExternalSystem FindServiceByMandantAndType(string externalServiceTypeName, SessionData sessionData);
    }
}
