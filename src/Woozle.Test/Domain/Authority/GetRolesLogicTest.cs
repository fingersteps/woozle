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
        private Model.User user;
        private SessionData sessionObject;
        private readonly Session session;
        private Mock<IRepository<UserMandatorRole>> userMandatorRoleRepositoryMock;

        public GetRolesLogicTest()
        {
            mandatorRoleRepositoryMock = new Mock<IRepository<MandatorRole>>();
            userMandatorRoleRepositoryMock = new Mock<IRepository<UserMandatorRole>>();

            getRolesLogic = new GetRolesLogic(mandatorRoleRepositoryMock.Object, userMandatorRoleRepositoryMock.Object);

            mandator = new Model.Mandator();
            user = new User();
            sessionObject = new SessionData(user, mandator);
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

        [Fact]
        public void GetUserRoles_SeveralRolesForSpecificMandator()
        {
            mandator.Id = 3;
            user.Id = 2;

            var userMandatorRoles = new List<UserMandatorRole>()
            {
                new UserMandatorRole()
                {
                    UserId = 1,
                    MandatorRole = new MandatorRole()
                    {
                        MandId = 1,
                        Role = new Role()
                        {
                            Name = "SpecialRole"
                        }
                    }
                },
                new UserMandatorRole()
                {
                    UserId = 2,
                    MandatorRole = new MandatorRole()
                    {
                        MandId = 1,
                        Role = new Role()
                        {
                            Name = "SpecialRole"
                        }
                    }
                },
                new UserMandatorRole()
                {
                    UserId = 2,
                    MandatorRole = new MandatorRole()
                    {
                        MandId = 3,
                        Role = new Role()
                        {
                            Name = "User"
                        }
                    }
                },
                new UserMandatorRole()
                {
                    UserId = 2,
                    MandatorRole = new MandatorRole()
                    {
                        MandId = 3,
                        Role = new Role()
                        {
                            Name = "Admin"
                        }
                    }
                }
            }.AsQueryable();
            userMandatorRoleRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<SessionData>()))
                .Returns(userMandatorRoles);

            var result = getRolesLogic.GetUserRoles(session.SessionObject);

            Assert.Equal(2, result.Count);
            Assert.Equal("User", result[0]);
            Assert.Equal("Admin", result[1]);
        }
    }
}
