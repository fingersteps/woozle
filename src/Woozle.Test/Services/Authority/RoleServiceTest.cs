using System.Collections.Generic;
using Moq;
using Woozle.Domain.ModuleManagement;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using Xunit;
using ModulePermissionsResult = Woozle.Model.ModulePermissions.ModulePermissionsResult;
using Role = Woozle.Model.Role;

namespace Woozle.Test.Services.Authority
{
    public class RoleServiceTest : SessionTestBase 
    {
        private readonly Mock<IModuleLogic> logic;

        public RoleServiceTest()
        {
            this.logic = new Mock<IModuleLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void GetModulePermissionsForRoleTest()
        {
            var permissions = new List<ModulePermissionsResult>()
                {
                    new ModulePermissionsResult() {ModuleId = 1},
                    new ModulePermissionsResult() {ModuleId = 2}
                };

            logic.Setup(n => n.FindModulePermissions(It.IsAny<Role>(), It.IsAny<SessionData>())).Returns(permissions);

            var service = this.CreateRoleService();
            var result = service.Get(new RoleModulePermissions());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].ModuleId);
            Assert.Equal(2, result[1].ModuleId);
        }

        private RoleService CreateRoleService()
        {
            var requestContextMock = this.GetFakeRequestContext();

            var roleService = new RoleService(this.logic.Object)
            {
                RequestContext = requestContextMock
            };

            return roleService;
        }
    }
}
