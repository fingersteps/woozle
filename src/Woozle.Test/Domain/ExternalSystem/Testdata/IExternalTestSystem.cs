using Woozle.Domain.ExternalSystem;

namespace Woozle.Test.Domain.ExternalSystem.Testdata
{
    public interface IExternalTestSystem : IExternalSystem
    {
        bool Test(int testParam);
    }
}
