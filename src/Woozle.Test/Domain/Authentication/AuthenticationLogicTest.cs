using System.Collections.Generic;
using System.Linq;
using Moq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Persistence;
using Woozle.Persistence.Ef;
using Xunit;

namespace Woozle.Test.Domain.Authentication
{
    public class AuthenticationLogicTest
    {
        private readonly IAuthenticationLogic authLogic;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IEfUnitOfWork> unitOfWorkMock;
        private readonly Mock<IHashProvider> passwordHasherMock;

        public AuthenticationLogicTest()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.unitOfWorkMock = new Mock<IEfUnitOfWork>();
            this.passwordHasherMock = new Mock<IHashProvider>();

            this.authLogic = new AuthenticationLogic(
                userRepositoryMock.Object, this.unitOfWorkMock.Object, this.passwordHasherMock.Object);
        }
       
        [Fact]
        public void LoginWithOneMandatorTest()
        {
            var exampleMandator = new Model.Mandator {Id = 0, Name = "Test"};

            var foundUser = new User
                                {
                                    Id = 0,
                                    FlagActive = true,
                                    Username = "sha",
                                    PasswordHash = "Hash",
                                    PasswordSalt = "Salt"
                                };

            var loginRequest = new LoginRequest
            {
                Mandator = exampleMandator,
                Username = "sha",
                Password = "correctPW"
            };

            var selectedSessionData = new SessionData(foundUser, exampleMandator);

            userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username))
                              .Returns(new UserSearchForLoginResult
                                         {
                                             Mandators = new List<Model.Mandator> {exampleMandator},
                                             User = foundUser
                                         });

            passwordHasherMock.Setup(
                n => n.VerifyHashString(loginRequest.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
                .Returns(true);

            var result = this.authLogic.Login(loginRequest);

            Assert.True(result.LoginSuccessful);
            Assert.False(result.CheckMandators);
            Assert.Null(result.SuggestedMandators);
            Assert.Equal(selectedSessionData, result.SessionData);

            this.userRepositoryMock.Verify(n => n.Save(foundUser, selectedSessionData));
            this.unitOfWorkMock.Verify(n => n.Commit(), Times.Once);
        }

        [Fact]
        public void LoginWithSeveralMandatorsTest()
        {
            var exampleMandator1 = new Model.Mandator {Id = 0, Name = "Test1"};
            var exampleMandator2 = new Model.Mandator {Id = 1, Name = "Test2"};

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

            userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username))
                .Returns(new UserSearchForLoginResult
                {
                    Mandators = new List<Model.Mandator> {exampleMandator1, exampleMandator2},
                    User = foundUser
                });

            passwordHasherMock.Setup(
                n => n.VerifyHashString(loginRequest.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
                .Returns(true);

            var result = this.authLogic.Login(loginRequest);

            Assert.False(result.LoginSuccessful);
            Assert.True(result.CheckMandators);
            Assert.NotNull(result.SuggestedMandators);

            Assert.Equal(0, result.SuggestedMandators.First().Id);
            Assert.Equal("Test1", result.SuggestedMandators.First().Name);
            Assert.Equal(1, result.SuggestedMandators.Last().Id);
            Assert.Equal("Test2", result.SuggestedMandators.Last().Name);
        }


        [Fact]
        public void LoginWithWrongUsername()
        {
            var loginRequest = new LoginRequest
            {
                Mandator = new Model.Mandator { Id = 0, Name = "Test" },
                Username = "wrongUsername",
                Password = "wrongPW"
            };

            Assert.Throws<InvalidLoginException>(() =>
            {
                authLogic.Login(loginRequest);
            });
        }

        [Fact]
        public void LoginWithWrongPassword()
        {
            var loginRequest = new LoginRequest
            {
                Mandator = new Model.Mandator {Id = 0, Name = "Test"},
                Username = "sha",
                Password = "wrongPW"
            };

            var foundUser = new User
            {
                Id = 0,
                FlagActive = true,
                Username = "sha"
            };

            userRepositoryMock.Setup(n => n.FindForLogin(loginRequest.Username))
                .Returns(new UserSearchForLoginResult
                {
                    Mandators = null,
                    User = foundUser
                });

            passwordHasherMock.Setup(
                n => n.VerifyHashString(loginRequest.Password, "", foundUser.PasswordSalt))
                .Returns(false);

            Assert.Throws<InvalidLoginException>(() =>
            {
                authLogic.Login(loginRequest);
            });
        }
    }
}
