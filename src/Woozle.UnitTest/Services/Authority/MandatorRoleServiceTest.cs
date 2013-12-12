using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Authority;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using MandatorRole = Woozle.Model.MandatorRole;

namespace Woozle.UnitTest.Services.Authority
{
    [TestClass]
    public class MandatorRoleServiceTest : SessionTestBase
    {
        private Mock<IGetRolesLogic> getRolesLogicMock;

        [TestInitialize]
        public void Initialize()
        {
            this.getRolesLogicMock = new Mock<IGetRolesLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
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

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }        
        
        [TestMethod]
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

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
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
