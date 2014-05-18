using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.Settings;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.Settings
{
    public class SettingsLogicTest
    {
        private readonly Mock<IRepository<Setting>> settingsRepositoryMock;
        private readonly SettingsLogic settingsLogic;
        private readonly IQueryable<Setting> settings;
        private readonly Session session;

        public SettingsLogicTest()
        {
            settingsRepositoryMock = new Mock<IRepository<Setting>>(MockBehavior.Strict);
            settingsLogic = new SettingsLogic(settingsRepositoryMock.Object);

            session = new Session( null);

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

        [Fact]
        public void LoadTest()
        {
            settingsRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(settings);
            var setting = settingsLogic.Load(session.SessionData);
            Assert.NotNull(setting);
            Assert.Equal(settings.First().EventManagementPlanningEMail, setting.EventManagementPlanningEMail);
            Assert.Equal(settings.First().EventManagementPlanningMobile, setting.EventManagementPlanningMobile);
        }

        [Fact]
        public void SaveTest()
        {
            var setting = new Setting() { Id = 2, EventManagementPlanningEMail = "Mail1", EventManagementPlanningMobile = "Mobile1" };

            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(n => n.Commit());
            settingsRepositoryMock.Setup(n => n.Save(setting, session.SessionData)).Returns(setting);
            settingsRepositoryMock.Setup(n => n.UnitOfWork).Returns(unitOfWorkMock.Object);

            var savedSetting = settingsLogic.Save(setting, session.SessionData);
            Assert.NotNull(setting);
            Assert.Equal(setting.EventManagementPlanningEMail, savedSetting.TargetObject.EventManagementPlanningEMail);
            Assert.Equal(setting.EventManagementPlanningMobile, savedSetting.TargetObject.EventManagementPlanningMobile);
        }
    }
}
