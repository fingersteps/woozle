using AutoMapper;
using Woozle.Services;
using Xunit;

namespace Woozle.Test.Services
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
