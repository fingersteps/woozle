using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woozle.Services;

namespace Woozle.UnitTest.Services
{
    [TestClass]
    public class MappingConfigurationTest
    {
        [TestMethod]
        public void InitializeMappingTest()
        {
            MappingConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
