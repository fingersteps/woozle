using System.Collections.Generic;
using Moq;
using Woozle.Domain.Authority;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using Xunit;
using MandatorRole = Woozle.Model.MandatorRole;

namespace Woozle.UnitTest.Services.Authority
{

    public class MandatorRoleServiceTest : SessionTestBase
    {
        private readonly Mock<IGetRolesLogic> getRolesLogicMock;

        public MandatorRoleServiceTest()
        {
            this.getRolesLogicMock = new Mock<IGetRolesLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void GetMandatorRolesTest()
        {
            var mandatorRoles = new List<MandatorRole>()
                {
                    new MandatorRole() {Id = 1},
                    new MandatorRole() {Id = 2}
                };

            getRolesLogicMock.Setup(n => n.GetMandatorRolesForMandator(It.IsAny<Session>())).Returns(mandatorRoles);

            var service = CreateService();
            var result = service.Get(new MandatorRoles());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
        }

        [Fact]
        public void GetMandatorRolesForDropDownTest()
        {
            var mandatorRoles = new List<MandatorRole>()
                {
                    new MandatorRole() {Id = 1},
                    new MandatorRole() {Id = 2}
                };

            getRolesLogicMock.Setup(n => n.GetAllMandatorRolesByMandator(It.IsAny<Session>())).Returns(mandatorRoles);

            var service = CreateService();
            var result = service.Get(new MandatorRolesForDropDown());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
        }


       private MandatorRoleService CreateService()
       {
           var requestContextMock = this.GetRequestContextMock();

           var mandatorRoleService = new MandatorRoleService(getRolesLogicMock.Object)
                                         {
                                             RequestContext =
                                                 requestContextMock.Object
                                         };

           return mandatorRoleService;
       }
    }
}
