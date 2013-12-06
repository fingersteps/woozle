//using System;
//using System.Collections.Generic;
//using System.Linq;
//using FluentValidation.Results;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Woozle.Core.BusinessLogic.Impl.UserManagement;
//using Woozle.Core.BusinessLogic.UserManagement;
//using Woozle.Core.Common.PermissionManagement;
//using Woozle.Core.Model;
//using Woozle.Core.Model.Query.UserSearch;
//using Woozle.Core.Model.SessionHandling;
//using Woozle.Core.Persistence.Repository;

//namespace Woozle.Core.BusinessLogic.Impl.Test.UserManagement
//{
//    [TestClass]
//    public class UserLogicTest
//    {
//        private IUserLogic userLogic;
//        private Mock<IUserRepository> userRepositoryMock;
//        private Mock<IUserValidator> userValidatorMock;
//        private Mock<IPermissionManager> permissionManagerMock;

//        [TestInitialize]
//        public void Initialize()
//        {
//            userRepositoryMock = new Mock<IUserRepository>();
//            userValidatorMock = new Mock<IUserValidator>();
//            permissionManagerMock = new Mock<IPermissionManager>();
//            permissionManagerMock.Setup(n => n.HasPermission(It.IsAny<SessionData>(), It.IsAny<string>(), It.IsAny<string>()))
//                                 .Returns(true);

//            userValidatorMock.Setup(n => n.Validate(It.IsAny<User>())).Returns(new ValidationResult());
//        }

//        [TestMethod]
//        public void UserSearchTest()
//        {
//            var results = new List<UserSearchResult>();

//            var userSearchResult1 = new UserSearchResult
//                                        {
//                                            Firstname = "Patrick",
//                                            Lastname = "Roos",
//                                            Id = 1,
//                                            Username = "pro"
//                                        };
            
//            results.Add(userSearchResult1);

//            userRepositoryMock.Setup(n => n.FindByUserCriteria(It.IsAny<UserSearchCriteria>(), It.IsAny<Session>()))
//                              .Returns(results);


//            this.userLogic = new UserLogic(userValidatorMock.Object, userRepositoryMock.Object, permissionManagerMock.Object);

//            var criteria = new UserSearchCriteria
//                               {
//                                   Firstname = "Patrick",
//                                   Lastname = "Roos",
//                                   Username = "pro"
//                               };


//            var result = this.userLogic.Search(criteria, 
//                            new Session(Guid.NewGuid(), 
//                            new SessionData(null, null), DateTime.Now.AddHours(10)));

//            Assert.IsNotNull(result);
//            Assert.AreEqual(results, result);
//            Assert.AreEqual(1, result[0].Id);
//        }

//        [TestMethod]
//        public void UserSearchWithNullableCriteriaTest()
//        {
//            this.userLogic = new UserLogic(userValidatorMock.Object, userRepositoryMock.Object, permissionManagerMock.Object);

//            var result = this.userLogic.Search(null,
//                            new Session(Guid.NewGuid(),
//                            new SessionData(null, null), DateTime.Now.AddHours(10)));

//            Assert.IsNull(result);
//        }


//        [TestMethod]
//        public void GetUsersForMandatorTest()
//        {
//            var session = new Session(Guid.NewGuid(), new SessionData(null, new Mandator() {Id = 1}),
//                                      DateTime.Now.AddHours(10));

//            IQueryable<User> users = new List<User>()
//                {
//                    new User()
//                        {
//                            Id = 1,
//                            UserMandatorRoles =
//                                new FixupCollection<UserMandatorRole>()
//                                    {
//                                        new UserMandatorRole()
//                                            {
//                                                MandatorRole = new MandatorRole() {MandId = 2, RoleId = 1},
//                                            },
//                                        new UserMandatorRole()
//                                            {
//                                                MandatorRole = new MandatorRole() {MandId = 1, RoleId = 3}
//                                            }


//                                    }
//                        },

//                    new User()
//                        {
//                            Id = 2,
//                            UserMandatorRoles =
//                                new FixupCollection<UserMandatorRole>()
//                                    {
//                                        new UserMandatorRole()
//                                            {
//                                                MandatorRole = new MandatorRole() {MandId = 2, RoleId = 1},
//                                            }

//                                    }
//                        },

//                    new User()
//                        {
//                            Id = 3,
//                            UserMandatorRoles =
//                                new FixupCollection<UserMandatorRole>()
//                                    {
//                                        new UserMandatorRole()
//                                            {
//                                                MandatorRole = new MandatorRole() {MandId = 1, RoleId = 4}
//                                            }

//                                    }
//                        }
//                }.AsQueryable();

//            userRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(users);

//            this.userLogic = new UserLogic(userValidatorMock.Object, userRepositoryMock.Object,
//                                           permissionManagerMock.Object);

//            var result = this.userLogic.GetUsersOfMandator(session);

//            Assert.IsNotNull(result);
//            Assert.AreEqual(2, result.Count);
//            Assert.AreEqual(1, result[0].Id);
//            Assert.AreEqual(3, result[1].Id);
//        }

//    }
//}
