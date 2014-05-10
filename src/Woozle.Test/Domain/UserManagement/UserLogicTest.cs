using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.PermissionManagement;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.UserManagement
{
    public class UserLogicTest
    {
        private IUserLogic userLogic;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IPermissionManager> permissionManagerMock;

        public UserLogicTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            permissionManagerMock = new Mock<IPermissionManager>();
            permissionManagerMock.Setup(n => n.HasPermission(It.IsAny<SessionData>(), It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(true);
            this.userLogic = new UserLogic(userRepositoryMock.Object,
                                     permissionManagerMock.Object);
        }

        [Fact]
        public void UserSearchTest()
        {
            var results = new List<UserSearchResult>();

            var userSearchResult1 = new UserSearchResult
                                        {
                                            Firstname = "Patrick",
                                            Lastname = "Roos",
                                            Id = 1,
                                            Username = "pro"
                                        };
            
            results.Add(userSearchResult1);

            userRepositoryMock.Setup(n => n.FindByUserCriteria(It.IsAny<UserSearchCriteria>(), It.IsAny<SessionData>()))
                              .Returns(results);

            var criteria = new UserSearchCriteria
                               {
                                   Firstname = "Patrick",
                                   Lastname = "Roos",
                                   Username = "pro"
                               };


            var result = this.userLogic.Search(criteria, 
                            new SessionData(null, null));

            Assert.NotNull(result);
            Assert.Equal(results, result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void UserSearchWithNullableCriteriaTest()
        {
            var result = this.userLogic.Search(null,
                            new SessionData(null, null));

            Assert.Null(result);
        }


        [Fact]
        public void GetUsersForMandatorTest()
        {
            var session = new Session(Guid.NewGuid(), new SessionData(null, new Model.Mandator() {Id = 1}),
                                      DateTime.Now.AddHours(10));

            IQueryable<User> users = new List<User>()
                {
                    new User()
                        {
                            Id = 1,
                            UserMandatorRoles =
                                new ObservableCollection<UserMandatorRole>
                                    {
                                        new UserMandatorRole
                                            {
                                                MandatorRole = new MandatorRole {MandId = 2, RoleId = 1},
                                            },
                                        new UserMandatorRole
                                            {
                                                MandatorRole = new MandatorRole {MandId = 1, RoleId = 3}
                                            }


                                    }
                        },

                    new User
                        {
                            Id = 2,
                            UserMandatorRoles =
                                new ObservableCollection<UserMandatorRole>
                                    {
                                        new UserMandatorRole
                                            {
                                                MandatorRole = new MandatorRole {MandId = 2, RoleId = 1},
                                            }

                                    }
                        },

                    new User()
                        {
                            Id = 3,
                            UserMandatorRoles =
                                new ObservableCollection<UserMandatorRole>
                                    {
                                        new UserMandatorRole
                                            {
                                                MandatorRole = new MandatorRole {MandId = 1, RoleId = 4}
                                            }

                                    }
                        }
                }.AsQueryable();

            userRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(users);

      

            var result = this.userLogic.GetUsersOfMandator(session.SessionData);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(3, result[1].Id);
        }
    }
}
