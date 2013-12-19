using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Woozle.Domain.ModuleManagement;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.UnitTest.Domain.ModuleManagement
{

    /// <summary>
    /// Tests for the ModuleLogic
    /// </summary>
    public class ModuleLogicTest
    {
        [Fact]
        public void GetSeveralModulesByMandatorTest()
        {
            var mandator = new Model.Mandator() {Id = 1, Name = "MyMandator"};
            var sessionData = new SessionData(null, mandator);
            var session = new Session(Guid.Empty, sessionData, DateTime.Now);

            var function1 = new Function() {Id = 1, LogicalId = "Function1"};
            var function2 = new Function() {Id = 2, LogicalId = "Function2"};
            var functions1 = new ObservableCollection<Function>() {function1, function2};

            var function3 = new Function() {Id = 3, LogicalId = "Function3"};
            var function4 = new Function() {Id = 4, LogicalId = "Function4"};
            var functions2 = new ObservableCollection<Function>() {function3, function4};


            var module1 = new ModuleForMandator() { Id = 1, Name = "Module1", Functions = functions1 };
            var module2 = new ModuleForMandator() { Id = 2, Name = "Module2", Functions = functions2 };

            var permissionManagerMock = new Mock<IPermissionManager>();
            permissionManagerMock.Setup(
                n => n.HasPermission(sessionData, function1.LogicalId, Permissions.PERMISSION_FUNCTION)).Returns(
                    true);
            permissionManagerMock.Setup(
                n => n.HasPermission(sessionData, function2.LogicalId, Permissions.PERMISSION_FUNCTION)).Returns(
                    false);

            permissionManagerMock.Setup(
                n => n.HasPermission(sessionData, function3.LogicalId, Permissions.PERMISSION_FUNCTION)).Returns(
                    false);
            permissionManagerMock.Setup(
                n => n.HasPermission(sessionData, function4.LogicalId, Permissions.PERMISSION_FUNCTION)).Returns(
                    false);


            var repositoryMock = new Mock<IModuleRepository>();
            repositoryMock.Setup(n => n.FindModulesForMandator(session)).Returns(
                    new List<ModuleForMandator> { module1, module2 });

            var moduleLogic = new ModuleLogic(permissionManagerMock.Object, repositoryMock.Object);
            var modules = moduleLogic.GetModulesByMandator(session);

            Assert.Equal(1, modules.Count);
            Assert.Equal(1, modules[0].Functions.Count);
            Assert.Equal(function1.LogicalId, modules[0].Functions[0].LogicalId);
        }
    }
}
