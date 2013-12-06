using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woozle.Core.Services.Stack.ServiceWoozle.Model.Mapping;

namespace Woozle.Core.Services.Stack.ServiceModel.Mapping.Test
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
