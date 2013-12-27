using System.Collections.Generic;
using Moq;
using Woozle.Domain.ModuleManagement;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Modules;
using Xunit;
using ModuleForMandator = Woozle.Model.ModulePermissions.ModuleForMandator;

namespace Woozle.UnitTest.Services.Modules
{
    public class ModuleServiceTest : SessionTestBase
    {
        private readonly Mock<IModuleLogic> moduleLogicMock;

        public ModuleServiceTest()
        {
            this.moduleLogicMock = new Mock<IModuleLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void FindAllTest()
        {
            var modules = new List<ModuleForMandator>()
                {
                    new ModuleForMandator() {Id = 1},
                    new ModuleForMandator() {Id = 2}
                };
            moduleLogicMock.Setup(n => n.GetModulesByMandator(It.IsAny<Session>())).Returns(modules);

            var service = CreateModuleService();
            var result = service.Get(new Woozle.Services.Modules.Modules());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        private ModuleService CreateModuleService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var moduleService = new ModuleService(moduleLogicMock.Object)
            {
                RequestContext =
                    requestContextMock.Object
            };

            return moduleService;
        }
    }
}
