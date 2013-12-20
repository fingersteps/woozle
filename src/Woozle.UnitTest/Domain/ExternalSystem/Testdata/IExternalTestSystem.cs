using Woozle.Domain.ExternalSystem;

namespace Woozle.UnitTest.Domain.ExternalSystem.Testdata
{
    public interface IExternalTestSystem : IExternalSystem
    {
        bool Test(int testParam);
    }
}
