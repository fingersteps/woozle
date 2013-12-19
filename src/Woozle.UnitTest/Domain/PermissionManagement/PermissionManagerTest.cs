using System.Collections.Generic;
using Moq;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Xunit;

namespace Woozle.UnitTest.Domain.PermissionManagement
{
    public class PermissionManagerTest
    {
        private readonly Mock<IPermissionProvider> permissionProviderMock;
        private readonly PermissionManager permissionManager;

        public PermissionManagerTest()
        {
            this.permissionProviderMock = new Mock<IPermissionProvider>();
            this.permissionManager = new PermissionManager(permissionProviderMock.Object);
        }

        [Fact]
        public void HasPermissionTest()
        {
            this.permissionProviderMock.Setup(n => n.GetAssignedPermissions(It.IsAny<SessionData>()))
                                        .Returns(new List<FunctionPermission>()
                                                     {
                                                         new FunctionPermission
                                                             {
                                                                 Function = new Function
                                                                                {
                                                                                    LogicalId = "1"
                                                                                },

                                                                 Permission = new Permission
                                                                                  {
                                                                                      LogicalId = "2"
                                                                                  }
                                                             },
                                                              new FunctionPermission
                                                             {
                                                                 Function = new Function
                                                                                {
                                                                                    LogicalId = "4"
                                                                                },

                                                                 Permission = new Permission
                                                                                  {
                                                                                      LogicalId = "3"
                                                                                  }
                                                             }
                                                     });

            Assert.True(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

        [Fact]
        public void HasNoPermissionTest()
        {
            this.permissionProviderMock.Setup(n => n.GetAssignedPermissions(It.IsAny<SessionData>()))
                                        .Returns(new List<FunctionPermission>()
                                                     {
                                                         new FunctionPermission
                                                             {
                                                                 Function = new Function
                                                                                {
                                                                                    LogicalId = "1"
                                                                                },

                                                                 Permission = new Permission
                                                                                  {
                                                                                      LogicalId = "2"
                                                                                  }
                                                             },
                                                              new FunctionPermission
                                                             {
                                                                 Function = new Function
                                                                                {
                                                                                    LogicalId = "2"
                                                                                },

                                                                 Permission = new Permission
                                                                                  {
                                                                                      LogicalId = "3"
                                                                                  }
                                                             }
                                                     });

            Assert.False(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

        [Fact]
        public void PermissionsNullTest()
        {
            Assert.False(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

    }
}
