using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.ModuleManagement;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using ModulePermissionsResult = Woozle.Model.ModulePermissions.ModulePermissionsResult;
using Role = Woozle.Model.Role;

namespace Woozle.UnitTest.Services.Authority
{
    [TestClass]
    public class RoleServiceTest : SessionTestBase 
    {
        private Mock<IModuleLogic> logic;

        [TestInitialize]
        public void Initialize()
        {
            this.logic = new Mock<IModuleLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
        public void GetModulePermissionsForRoleTest()
        {
            var permissions = new List<ModulePermissionsResult>()
                {
                    new ModulePermissionsResult() {ModuleId = 1},
                    new ModulePermissionsResult() {ModuleId = 2}
                };

            logic.Setup(n => n.FindModulePermissions(It.IsAny<Role>(), It.IsAny<Session>())).Returns(permissions);

            var service = this.CreateRoleService();
            var result = service.Get(new RoleModulePermissions());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].ModuleId);
            Assert.AreEqual(2, result[1].ModuleId);
        }

        private RoleService CreateRoleService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var roleService = new RoleService(this.logic.Object)
            {
                RequestContext =
                    requestContextMock.Object
            };

            return roleService;
        }
    }
}
