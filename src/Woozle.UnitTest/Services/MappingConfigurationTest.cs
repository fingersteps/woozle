using AutoMapper;
using Woozle.Services;
using Xunit;

namespace Woozle.UnitTest.Services
{
    public class MappingConfigurationTest
    {
        [Fact]
        public void InitializeMappingTest()
        {
            MappingConfiguration.Configure();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
