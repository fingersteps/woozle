using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Settings;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.Settings;

namespace Woozle.UnitTest.Services.Settings
{
    [TestClass]
    public class SettingServiceTest : SessionTestBase
    {
        private Mock<ISettingsLogic> SettingLogicMock;

        private Setting modelSetting;

        [TestInitialize]
        public void Initialize()
        {
            modelSetting = new Setting() { Id = 1 };

            this.SettingLogicMock = new Mock<ISettingsLogic>();
            MappingConfiguration.Configure();
        }

   
        [TestMethod]
        public void LoadTest()
        {
            SettingLogicMock.Setup(n => n.Load(It.IsAny<Session>())).Returns(modelSetting);

            var service = CreateSettingService();
            var result = service.Get(new Setting());

            Assert.IsNotNull(result);
        }

       //[TestMethod]
       // public void InsertTest()
       // {
       //     MockSave();

       //     var service = CreateSettingService();
       //     var result = service.Post(new Setting() { Id = 1 });

       //     Assert.IsNotNull(result);
       //     Assert.IsNotNull(result.TargetObject);
       //     Assert.AreEqual(1, result.TargetObject.Id);
       // }

       // [TestMethod]
       // public void UpdateTest()
       // {
       //     MockSave();

       //     var service = CreateSettingService();
       //     var result = service.Put(new Setting() { Id = 1 });

       //     Assert.IsNotNull(result);
       //     Assert.IsNotNull(result.TargetObject);
       //     Assert.AreEqual(1, result.TargetObject.Id);
       // }

        private SettingService CreateSettingService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var settingService = new SettingService(SettingLogicMock.Object)
            {
                RequestContext =
                    requestContextMock.Object
            };

            return settingService;
        }

        private void MockSave()
        {
            var modelSaveResult = new SaveResult<Setting>() { TargetObject = modelSetting };
            SettingLogicMock.Setup(n => n.Save(It.IsAny<Setting>(), It.IsAny<Session>())).Returns(modelSaveResult);
        }
    }
}
