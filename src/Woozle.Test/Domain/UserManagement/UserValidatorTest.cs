using System;
using Woozle.Domain.UserManagement;
using Xunit;

namespace Woozle.Test.Domain.UserManagement
{
    public class UserValidatorTest
    {
        private UserValidator validator;

        public UserValidatorTest()
        {
            this.validator = new UserValidator();
        }

        [Fact]
        public void ValidateMissingUsernameTest()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.ValidateNewUser("", "eMail"));
        }

        [Fact]
        public void ValidateUserWithMissingEMailTest()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.ValidateNewUser("user", ""));
        }

        [Fact]
        public void ValidateUserWithWrongUsernameTest()
        {
            Assert.Throws<ArgumentException>(() => this.validator.ValidateNewUser("a", "eMail"));
        }

        [Fact]
        public void ValidateUserWithMissingPasswordTest()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.ValidateUserPassword(""));
        }

        [Fact]
        public void ValidateUserWithWrongPasswordTest()
        {
            Assert.Throws<ArgumentException>(() => this.validator.ValidateUserPassword("test"));
        }

        [Fact]
        public void ValidateUserWithCorrectPasswordTest()
        {
            this.validator.ValidateUserPassword("test123");
        }
    }
}
