using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.MandatorManagement;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.Mandator;

namespace Woozle.UnitTest.Services.Mandator
{
    [TestClass]
    public class MandatorServiceTest : SessionTestBase
    {
        private Mock<IMandatorLogic> mandatorLogicMock;

        private Model.Mandator modelMandator;

        [TestInitialize]
        public void Initialize()
        {
            modelMandator = new Model.Mandator { Id = 1 };

            this.mandatorLogicMock = new Mock<IMandatorLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
        public void LoadTest()
        {
            mandatorLogicMock.Setup(n => n.LoadMandator(It.IsAny<Session>())).Returns(modelMandator);

            var service = CreateMandatorService();
            var result = service.Get(new MandatorDto { Id = 1 });

            Assert.IsNotNull(result);
        }
       

        [TestMethod]
        public void UpdateTest()
        {
            MockSave();

            var service = CreateMandatorService();
            var result = service.Put(new MandatorDto() { Id = 1 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TargetObject);
            Assert.AreEqual(1, result.TargetObject.Id);
        }

        private MandatorService CreateMandatorService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var mandatorService = new MandatorService(mandatorLogicMock.Object)
            {
                RequestContext =
                    requestContextMock.Object
            };

            return mandatorService;
        }

        private void MockSave()
        {
            var modelSaveResult = new SaveResult<Model.Mandator>() { TargetObject = modelMandator };
            mandatorLogicMock.Setup(n => n.Save(It.IsAny<Model.Mandator>(), It.IsAny<Session>())).Returns(modelSaveResult);
        }
    }
}
