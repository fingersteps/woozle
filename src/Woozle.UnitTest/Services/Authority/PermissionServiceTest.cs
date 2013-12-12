using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Authority;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using Role = Woozle.Model.Role;

namespace Woozle.UnitTest.Services.Authority
{
    [TestClass]
    public class PermissionServiceTest : SessionTestBase
    {
        private Mock<IPermissionsLogic> permissionLogicMock;

        [TestInitialize]
        public void Initialize()
        {
            this.permissionLogicMock = new Mock<IPermissionsLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
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

            Assert.IsNotNull(result);
        }


        [TestMethod]
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
