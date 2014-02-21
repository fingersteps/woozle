using Moq;
using Woozle.Domain.Settings;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.Settings;
using Xunit;

namespace Woozle.Test.Services.Settings
{
    public class SettingServiceTest : SessionTestBase
    {
        private readonly Mock<ISettingsLogic> SettingLogicMock;
        private readonly Setting modelSetting;

        public SettingServiceTest()
        {
            modelSetting = new Setting() { Id = 1 };

            this.SettingLogicMock = new Mock<ISettingsLogic>();
            MappingConfiguration.Configure();
        }
   
        [Fact]
        public void LoadTest()
        {
            SettingLogicMock.Setup(n => n.Load(It.IsAny<SessionData>())).Returns(modelSetting);

            var service = CreateSettingService();
            var result = service.Get(new Setting());

            Assert.NotNull(result);
        }

       [Fact]
        public void InsertTest()
        {
            MockSave();

            var service = CreateSettingService();
            var result = service.Post(new Setting() { Id = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.TargetObject);
            Assert.Equal(1, result.TargetObject.Id);
        }

        [Fact]
        public void UpdateTest()
        {
            MockSave();

            var service = CreateSettingService();
            var result = service.Put(new Setting() { Id = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.TargetObject);
            Assert.Equal(1, result.TargetObject.Id);
        }

        private SettingService CreateSettingService()
        {
            var requestContextMock = this.GetFakeRequestContext();

            var settingService = new SettingService(SettingLogicMock.Object)
            {
                RequestContext =
                    requestContextMock
            };

            return settingService;
        }

        private void MockSave()
        {
            var modelSaveResult = new SaveResult<Setting>() { TargetObject = modelSetting };
            SettingLogicMock.Setup(n => n.Save(It.IsAny<Setting>(), It.IsAny<SessionData>())).Returns(modelSaveResult);
        }
    }
}
