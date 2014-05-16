using System;
using Moq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Xunit;

namespace Woozle.Test.Domain.PasswordChange
{
    public class PasswordChangeLogicTest
    {
        private readonly Mock<IUserLogic> userLogicMock;
        private readonly Mock<IUserValidator> userValidatorMock;
        private SaltedHash hashProvider;
        private readonly PasswordChangeLogic logic;

        public PasswordChangeLogicTest()
        {
            userLogicMock = new Mock<IUserLogic>();
            userValidatorMock = new Mock<IUserValidator>();
            hashProvider = new SaltedHash();
            this.logic = new PasswordChangeLogic(
                hashProvider, userValidatorMock.Object, userLogicMock.Object);
        }

        [Fact]
        public void ChangePasswordWithWrongOldPasswordTest()
        {
            var user = new User
            {
                Username = "abcde",
                Email = "test",
                PasswordHash = "97RhIcjrsCviqG/ExjKXuvT3tLknq0nRflunO/rFGSs=",
                PasswordSalt = "Kw7EgQ=="
            };

            Assert.Throws<ArgumentException>(() => this.logic.ChangePassword("wrongPassword", "newPassword", new SessionData(user, null)));
        }

        [Fact]
        public void ChangePasswordWithInvalidNewPasswordTest()
        {
            userValidatorMock.Setup(n => n.ValidateUserPassword("1")).Throws<ArgumentException>();
            var user = new User
            {
                Username = "abcde",
                Email = "test",
                PasswordHash = "97RhIcjrsCviqG/ExjKXuvT3tLknq0nRflunO/rFGSs=",
                PasswordSalt = "Kw7EgQ=="
            };

            Assert.Throws<ArgumentException>(() => this.logic.ChangePassword("tia$123", "1", new SessionData(user, null)));

            userValidatorMock.Verify();
        }

        [Fact]
        public void ChangePasswordWithValidNewPasswordTest()
        {
            var user = new User
            {
                Id = 1,
                Username = "abcde",
                Email = "test",
                PasswordHash = "97RhIcjrsCviqG/ExjKXuvT3tLknq0nRflunO/rFGSs=",
                PasswordSalt = "Kw7EgQ=="
            };
            var sessionData = new SessionData(user, null);

            this.userLogicMock.Setup(n => n.Save(user, sessionData)).Returns(user);

            this.logic.ChangePassword("tia$123", "test123", sessionData);
        }
    }
}
