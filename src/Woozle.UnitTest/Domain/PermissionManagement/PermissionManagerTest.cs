using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Core.BusinessLogic.PermissionManagement;
using Woozle.Core.Common.PermissionManagement;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.Common.PermissionManagement.Impl.Test
{
    [TestClass]
    public class PermissionManagerTest
    {
        private Mock<IPermissionProvider> permissionProviderMock;
        private PermissionManager permissionManager;
        
        [TestInitialize]
        public void Initialize()
        {
            this.permissionProviderMock = new Mock<IPermissionProvider>();   
            this.permissionManager = new PermissionManager(permissionProviderMock.Object);
        }

        [TestMethod]
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

            Assert.IsTrue(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

        [TestMethod]
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

            Assert.IsFalse(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

        [TestMethod]
        public void PermissionsNullTest()
        {
            Assert.IsFalse(this.permissionManager.HasPermission(new SessionData(null, null), "4", "3"));
        }

    }
}
