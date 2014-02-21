using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Test.Domain.ExternalSystem.Testdata
{
    public class TestExternalSystemRepository : IExternalSystemRepository
    {
        public Woozle.Model.ExternalSystem FindServiceByMandantAndType(string externalServiceTypeName, SessionData sessionData)
        {
            return null;
        }
    }
}
