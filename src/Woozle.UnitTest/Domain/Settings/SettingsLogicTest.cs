using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Settings;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Woozle.Persistence.Repository;

namespace Woozle.UnitTest.Domain.Settings
{
    [TestClass]
    public class SettingsLogicTest
    {
        private Mock<IRepository<Setting>> settingsRepositoryMock;
        private SettingsLogic settingsLogic;
        private IQueryable<Setting> settings;
        private Session session;

        [TestInitialize]
        public void Initialize()
        {
            settingsRepositoryMock = new Mock<IRepository<Setting>>(MockBehavior.Strict);
            settingsLogic = new SettingsLogic(settingsRepositoryMock.Object);

            session = new Session(Guid.NewGuid(), null, DateTime.Now);

            settings = new List<Setting>
                           {
                               new Setting()
                                   {
                                       Id = 1,
                                       EventManagementPlanningEMail = "mailAdr",
                                       EventManagementPlanningMobile = "mobileNumber"
                                   }
                           }.AsQueryable();
        }

        [TestMethod]
        public void LoadTest()
        {
            settingsRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(settings);
            var setting = settingsLogic.Load(session);
            Assert.IsNotNull(setting);
            Assert.AreEqual(settings.First().EventManagementPlanningEMail, setting.EventManagementPlanningEMail);
            Assert.AreEqual(settings.First().EventManagementPlanningMobile, setting.EventManagementPlanningMobile);
        }

        [TestMethod]
        public void SaveTest()
        {
            var setting = new Setting() { Id = 2, EventManagementPlanningEMail = "Mail1", EventManagementPlanningMobile = "Mobile1" };

            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(n => n.Commit());
            settingsRepositoryMock.Setup(n => n.Save(setting, session)).Returns(setting);
            settingsRepositoryMock.Setup(n => n.UnitOfWork).Returns(unitOfWorkMock.Object);

            var savedSetting = settingsLogic.Save(setting, session);
            Assert.IsNotNull(setting);
            Assert.AreEqual(setting.EventManagementPlanningEMail, savedSetting.TargetObject.EventManagementPlanningEMail);
            Assert.AreEqual(setting.EventManagementPlanningMobile, savedSetting.TargetObject.EventManagementPlanningMobile);
        }
    }
}
