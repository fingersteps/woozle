using System.Collections.Generic;
using Moq;
using Woozle.Domain.Authority;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using Xunit;
using Role = Woozle.Model.Role;

namespace Woozle.UnitTest.Services.Authority
{
    public class PermissionServiceTest : SessionTestBase
    {
        private readonly Mock<IPermissionsLogic> permissionLogicMock;

        public PermissionServiceTest()
        {
            this.permissionLogicMock = new Mock<IPermissionsLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void GetAllPermissionsTest()
        {
            var permissions = new List<FunctionPermission>()
                {
                    new FunctionPermission() {Id = 1},
                    new FunctionPermission() {Id = 2}
                };

            permissionLogicMock.Setup(n => n.GetAssignedPermissions(It.IsAny<Session>())).Returns(permissions);

            var service = CreatePermissionService();
            var result = service.Get(new Permissions());

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateTest()
        {
            permissionLogicMock.Setup(
                n =>
                n.SaveChangedPermissions(It.IsAny<Role>(), It.IsAny<List<Woozle.Model.ModulePermissions.ChangedModulePermission>>(),
                                         It.IsAny<Session>()));
            var service = CreatePermissionService();
            service.Put(new SavePermissions()
                {
                    Role = new Woozle.Services.Authority.Role() {Id = 1},
                    ChangedPermissions = new List<ChangedModulePermission>()
                });

            permissionLogicMock.Verify();
        }

        private PermissionService CreatePermissionService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var permissionService = new PermissionService(permissionLogicMock.Object)
            {
                RequestContext = requestContextMock.Object
            };

            return permissionService;
        }
    }
}
