using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.Numerator;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.Numerator
{
    public class NumberProviderTest
    {
        private NumberProvider numberProvider;
        private Mock<IRepository<NumberRange>> numberRangeRepositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private SessionData sessionData;

        public NumberProviderTest()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.numberRangeRepositoryMock = new Mock<IRepository<NumberRange>>();
            this.numberRangeRepositoryMock.Setup(n => n.UnitOfWork)
                .Returns(this.unitOfWorkMock.Object);
            this.numberProvider = new NumberProvider(numberRangeRepositoryMock.Object);

            sessionData = new SessionData(new User(), new Model.Mandator() {Id = 1});
        }

        [Fact]
        public void GetNumberTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 1,
                                                  Till = 10,
                                                  Current = 2,
                                                  MandatorId = 1
                                              },
                                          new NumberRange
                                              {
                                                  Id = 2,
                                                  Name = "NumberRange2",
                                                  From = 5,
                                                  Till = 25,
                                                  Current = 7,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);


            var number = this.numberProvider.GetNextNumber("NumberRange1", sessionData);

            Assert.Equal("3", number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }


        [Fact]
        public void GetNumberFromTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 1,
                                                  Till = 10,
                                                  Current = null,
                                                  MandatorId = 1
                                              },
                                          new NumberRange
                                              {
                                                  Id = 2,
                                                  From = 5,
                                                  Till = 25,
                                                  Current = 7,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);


         
            var number = this.numberProvider.GetNextNumber("NumberRange1", sessionData);

            Assert.Equal("1", number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }


        [Fact]
        public void GetNumberWithCurrentNullTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 1,
                                                  Till = 10,
                                                  Current = null,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);

       

            var number = this.numberProvider.GetNextNumber("NumberRange1", sessionData);

            Assert.Equal("1", number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }


        [Fact]
        public void GetNumberWithSpecificFormatTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 1,
                                                  Till = 10,
                                                  Current = null,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);


            var number = this.numberProvider.GetNextNumber("NumberRange1", "{0:00}", sessionData);

            Assert.Equal("01", number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }

        [Fact]
        public void GetNumberWithEmptyFormatStringAsDefaultTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 1,
                                                  Till = 10,
                                                  Current = null,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);



            var number = this.numberProvider.GetNextNumber("NumberRange1", string.Empty, sessionData);

            Assert.Equal("1", number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }

        [Fact]
        public void GetNumberWithNoNumberRangeFoundTest()
        {
            var numberRangeList = new List<NumberRange>().AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);



            var result = this.numberProvider.GetNextNumber("NotFound", string.Empty, sessionData);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetNumberTillCheckTest()
        {
            var numberRangeList = new List<NumberRange>
                                      {
                                          new NumberRange
                                              {
                                                  Id = 1,
                                                  Name = "NumberRange1",
                                                  From = 2,
                                                  Till = 4,
                                                  Current = 4,
                                                  MandatorId = 1
                                              },
                                          new NumberRange
                                              {
                                                  Id = 2,
                                                  From = 5,
                                                  Till = 25,
                                                  Current = 7,
                                                  MandatorId = 1
                                              }
                                      }.AsQueryable();

            this.numberRangeRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(numberRangeList);


          

            var number = this.numberProvider.GetNextNumber("NumberRange1", sessionData);
            Assert.Equal(string.Empty, number);
            numberRangeRepositoryMock.Verify(n => n.CreateQueryable(sessionData), Times.Once());
        }
    }
}
