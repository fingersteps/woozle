using System.ComponentModel.Composition;
using Woozle.Domain.ExternalSystem;

namespace Woozle.UnitTest.Domain.ExternalSystem.Testdata
{

    [Export(typeof(IExternalTestSystem))]
    [ExternalSystem(Name = "TestSystemV1")]
    public class ExternalTestSystem : IExternalTestSystem
    {
        #region IExternalTestSystem Members

        public bool Test(int testParam)
        {
            return testParam > 0;
        }

        #endregion
    }
}
