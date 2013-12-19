using Moq;
using Woozle.Domain.MandatorManagement;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.Mandator;
using Xunit;

namespace Woozle.UnitTest.Services.Mandator
{
    public class MandatorServiceTest : SessionTestBase
    {
        private readonly Mock<IMandatorLogic> mandatorLogicMock;
        private readonly Model.Mandator modelMandator;

 
        public MandatorServiceTest()
        {
            modelMandator = new Model.Mandator { Id = 1 };

            this.mandatorLogicMock = new Mock<IMandatorLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void LoadTest()
        {
            mandatorLogicMock.Setup(n => n.LoadMandator(It.IsAny<Session>())).Returns(modelMandator);

            var service = CreateMandatorService();
            var result = service.Get(new MandatorDto { Id = 1 });

            Assert.NotNull(result);
        }
       
        [Fact]
        public void UpdateTest()
        {
            MockSave();

            var service = CreateMandatorService();
            var result = service.Put(new MandatorDto() { Id = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.TargetObject);
            Assert.Equal(1, result.TargetObject.Id);
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
