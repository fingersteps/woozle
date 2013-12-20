using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.UnitTest.Domain.ExternalSystem.Testdata
{
    public class TestExternalSystemRepository : IExternalSystemRepository
    {
        public Woozle.Model.ExternalSystem FindServiceByMandantAndType(string externalServiceTypeName, Session session)
        {
            return null;
        }
    }
}
