using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.Authority;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.Authority
{
    public class GetRolesLogicTest
    {
        private readonly IGetRolesLogic getRolesLogic;
        private readonly Mock<IRepository<MandatorRole>> mandatorRoleRepositoryMock;
        private readonly IQueryable<MandatorRole> mandatorRoles;
        private Model.Mandator mandator;
        private SessionData sessionObject;
        private readonly Session session;

        public GetRolesLogicTest()
        {
            mandatorRoleRepositoryMock = new Mock<IRepository<MandatorRole>>();
            getRolesLogic = new GetRolesLogic(mandatorRoleRepositoryMock.Object);

            mandator = new Model.Mandator();
            sessionObject = new SessionData(null, mandator);
            session = new Session(Guid.NewGuid(), sessionObject, DateTime.Now.AddDays(1));

            mandatorRoles = new List<MandatorRole>
                            {
                                    new MandatorRole
                                    {
                                            MandId = 1,
                                            Mandator = new Model.Mandator {Id = 1, MandatorGroupId = 1},
                                            Role = new Role {Id = 1}
                                        },
                                    new MandatorRole
                                    {
                                            MandId = 2,
                                            Mandator = new Model.Mandator {Id = 2, MandatorGroupId = 1},
                                            Role = new Role {Id = 1}
                                        },
                                    new MandatorRole
                                    {
                                            MandId = 3,
                                            Mandator = new Model.Mandator {Id = 3},
                                            Role = new Role {Id = 1}
                                        }
                                }.AsQueryable();
        }


        [Fact]
        public void GetAllMandatorRolesByMandatorGroupTest()
        {
            session.SessionObject.Mandator.Id = 1;
            session.SessionObject.Mandator.MandatorGroupId = 1;

            this.mandatorRoleRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>()))
                .Returns(mandatorRoles);

            var result = getRolesLogic.GetAllMandatorRolesByMandator(session);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].MandId);
            Assert.NotNull(result[0].Mandator);
            Assert.NotNull(result[0].Role);
            Assert.Equal(2, result[1].MandId);
            Assert.NotNull(result[1].Mandator);
            Assert.NotNull(result[1].Role);
        }

        [Fact]
        public void GetAllMandatorRolesByMandatorWithoutGroupTest()
        {
            session.SessionObject.Mandator.Id = 3;

            this.mandatorRoleRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>()))
                .Returns(mandatorRoles);

            var result = getRolesLogic.GetAllMandatorRolesByMandator(session);

            Assert.Equal(1, result.Count);
            Assert.Equal(3, result[0].MandId);
            Assert.NotNull(result[0].Mandator);
            Assert.NotNull(result[0].Role);
        }
    }
}
