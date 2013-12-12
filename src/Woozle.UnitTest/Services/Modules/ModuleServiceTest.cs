using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.ModuleManagement;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Modules;

namespace Woozle.UnitTest.Services.Modules
{
    [TestClass]
    public class ModuleServiceTest : SessionTestBase
    {
        private Mock<IModuleLogic> moduleLogicMock;

        [TestInitialize]
        public void Initialize()
        {
            this.moduleLogicMock = new Mock<IModuleLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
        public void FindAllTest()
        {
            var modules = new List<ModuleForMandator>()
                {
                    new ModuleForMandator() {Id = 1},
                    new ModuleForMandator() {Id = 2}
                };
            moduleLogicMock.Setup(n => n.GetModulesByMandator(It.IsAny<Session>())).Returns(modules);

            var service = CreateModuleService();
            var result = service.Get(new ModulesDto());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
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
