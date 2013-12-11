using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Persistence.Repository;

namespace Woozle.UnitTest.Domain.Authentication
{
    [TestClass]
    public class AuthenticationLogicTest
    {
        private IAuthenticationLogic authLogic;
        private Mock<IUserRepository> userRepositoryMock;

        [TestInitialize]
        public void InitializeTest()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();

            this.authLogic = new AuthenticationLogic(userRepositoryMock.Object);

        }

        [TestMethod]
        public void LoginWithOneMandatorTest()
        {
            var exampleMandator = new Model.Mandator {Id = 0, Name = "Test"};

            var foundUser = new User
                                {
                                    Id = 0,
                                    FlagActive = true,
                                    Username = "sha"
                                };

            var loginRequest = new LoginRequest
            {
                Mandator = exampleMandator,
                Username = "sha",
                Password = "correctPW"
            };

            var selectedSessionData = new SessionData(foundUser, exampleMandator);

            userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username, loginRequest.Password, It.IsAny<Session>()))
                              .Returns(new UserSearchForLoginResult
                                         {
                                             Mandators = new List<Model.Mandator> {exampleMandator},
                                             User = foundUser
                                         });

            var result = this.authLogic.Login(loginRequest);

            Assert.IsTrue(result.LoginSuccessful);
            Assert.IsFalse(result.CheckMandators);
            Assert.IsNull(result.SuggestedMandators);
            Assert.AreEqual(selectedSessionData, result.SessionData);
        }

        [TestMethod]
        public void LoginWithSeveralMandatorsTest()
         {


             var exampleMandator1 = new Model.Mandator { Id = 0, Name = "Test1" };
             var exampleMandator2 = new Model.Mandator { Id = 1, Name = "Test2" };

             var foundUser = new User
             {
                 Id = 0,
                 FlagActive = true,
                 Username = "sha"
             };

             var loginRequest = new LoginRequest
             {
                 Username = "sha",
                 Password = "correctPW"
             };

             userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username, loginRequest.Password, It.IsAny<Session>()))
                            .Returns(new UserSearchForLoginResult
                            {
                                Mandators = new List<Model.Mandator> { exampleMandator1, exampleMandator2 },
                                User = foundUser
                            });

             var result = this.authLogic.Login(loginRequest);

             Assert.IsFalse(result.LoginSuccessful);
             Assert.IsTrue(result.CheckMandators);
             Assert.IsNotNull(result.SuggestedMandators);
             
            Assert.AreEqual(0, result.SuggestedMandators.First().Id);
            Assert.AreEqual("Test1", result.SuggestedMandators.First().Name);
            Assert.AreEqual(1, result.SuggestedMandators.Last().Id);
            Assert.AreEqual("Test2", result.SuggestedMandators.Last().Name);
         }



        [TestMethod]
        [ExpectedException(typeof(InvalidLoginException))]
        public void IncorrectLoginTest()
        {
            var loginRequest = new LoginRequest
            {
                Mandator = new Model.Mandator { Id = 0, Name = "Test" },
                Username = "sha",
                Password = "wrongPW"
            };

            this.authLogic.Login(loginRequest);
        }

        [TestMethod]
        public void LoginWithOneMandatorUserIsInactiveTest()
        {
            var exampleMandator = new Model.Mandator { Id = 0, Name = "Test" };

            var foundUser = new User
            {
                Id = 0,
                FlagActive = false,
                Username = "sha"
            };

            var loginRequest = new LoginRequest
            {
                Mandator = exampleMandator,
                Username = "sha",
                Password = "correctPW"
            };

            userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username, loginRequest.Password, It.IsAny<Session>()))
                              .Returns(new UserSearchForLoginResult
                              {
                                  Mandators = new List<Model.Mandator> { exampleMandator },
                                  User = foundUser
                              });


            var result = this.authLogic.Login(loginRequest);

            Assert.IsFalse(result.LoginSuccessful);
            Assert.IsFalse(result.CheckMandators);
            Assert.IsNull(result.SuggestedMandators);
        }
    }
}
