using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.MandatorManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.Mandator
{
    public class MandatorLogicTest
    {
        private readonly MandatorLogic mandatorLogic;
        private readonly Mock<IRepository<Model.Mandator>> mandatorRepository;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        public MandatorLogicTest()
        {
            this.mandatorRepository = new Mock<IRepository<Model.Mandator>>();
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.mandatorLogic = new MandatorLogic(mandatorRepository.Object);
        }

        [Fact]
        public void LoadMandatorTest()
        {

            var expectedMandator = new Model.Mandator
                                     {
                                         Id = 2,
                                         Name = "Test Mandant 2",
                                         Street = "Teststrasse 12",
                                         CityId = 75
                                     };

            this.mandatorRepository.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                                .Returns(new List<Model.Mandator>
                                             {
                                                 new Model.Mandator
                                                     {
                                                         Id = 1,
                                                         Name = "Test Mandant 1",
                                                         Street = "Teststrasse 10",
                                                         CityId = 123
                                                     },
                                                 expectedMandator
                                             }.AsQueryable());

            var mandator = new Model.Mandator
                               {
                                   Id = 2
                               };

            var session = new Session(Guid.NewGuid(), new SessionData(new User(), mandator), DateTime.Now);

            var result = this.mandatorLogic.LoadMandator(session.SessionData);
            Assert.Equal(expectedMandator, result);
        }

        [Fact]
        public void SaveTest()
        {
            var saveMandator = new Model.Mandator
                                   {
                                       Id = 1,
                                       Name = "Test Mandant"
                                   };

            this.mandatorRepository.Setup(n => n.Synchronize(saveMandator, It.IsAny<SessionData>()))
                                    .Returns(saveMandator);

            this.mandatorRepository.Setup(n => n.UnitOfWork)
                                    .Returns(this.unitOfWorkMock.Object);

            var result = this.mandatorLogic.Save(saveMandator,
                new SessionData(new User(), new Model.Mandator()));

            Assert.Equal(saveMandator, result.TargetObject);
            Assert.False(result.HasSystemErrors);

            mandatorRepository.Verify(n => n.Synchronize(saveMandator, It.IsAny<SessionData>()), Times.Once());
        }
    }
}
