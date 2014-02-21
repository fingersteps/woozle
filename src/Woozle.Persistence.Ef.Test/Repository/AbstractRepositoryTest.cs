using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Model;
using Woozle.Persistence.Ef.Repository;
using Xunit;

namespace Woozle.Persistence.Ef.Test.Repository
{
    /// <summary>
    /// Tests the <see cref="AbstractRepository{T}"/> by performing the methods of one concrete repository.
    /// </summary>
    public class AbstractRepositoryTest : RepositoryTestBase
    {
        private IList<City> cityData;
        private Mock<IEfUnitOfWork> unitofworkMock;

        public AbstractRepositoryTest()
        {
            InitializeData();
        }


        public void InitializeData()
        {
            this.cityData = new List<City>()
                {
                    new City()
                        {
                            Id = 1,
                            Name = "Willisau"
                        },
                    new City()
                        {
                            Id = 2,
                            Name = "Hergiswil"
                        },
                    new City()
                        {
                            Id = 3,
                            Name = "Luzern"
                        }
                };
        }

        [Fact]
        public void CountTest()
        {
            MockUnitOfWorkForCity();

            var cityRepo = new CityRepository(unitofworkMock.Object);
            var result = cityRepo.Count(this.Session.SessionData);

            Assert.Equal(3, result);
        }

        [Fact]
        public void FindAllTest()
        {
            MockUnitOfWorkForCity();

            var cityRepo = new CityRepository(unitofworkMock.Object);
            var result = cityRepo.FindAll(this.Session.SessionData);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void FindByIdTest()
        {
            const int primaryKeyToSearch = 1;
            var unitofworkMock = new Mock<IEfUnitOfWork>();
            unitofworkMock.Setup(n => n.LoadRelatedData<City>(primaryKeyToSearch, null, null)).Returns(this.cityData[0]);

            var cityRepo = new CityRepository(unitofworkMock.Object);
            var result = cityRepo.FindById(primaryKeyToSearch);

            Assert.Equal("Willisau", result.Name);
        }

        [Fact]
        public void FindByExpTest()
        {
            MockUnitOfWorkForCity();

            var cityRepo = new CityRepository(unitofworkMock.Object);
            var result = cityRepo.FindByExp(city => city.Name == "Luzern", this.Session.SessionData);

            Assert.Equal(1, result.Count);
            Assert.Equal(3, result[0].Id);
        }

        private void MockUnitOfWorkForCity()
        {
            unitofworkMock = new Mock<IEfUnitOfWork>();
            unitofworkMock.Setup(n => n.Get<City>(this.Session.SessionData)).Returns(this.cityData.AsQueryable());
        }
    }
}
