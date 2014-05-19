using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.PasswordRequest;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.PasswordRequest
{
    public class PasswordRequestValidatorTest
    {
        private readonly PasswordRequestValidator passwordRequestValidator;
        private readonly Mock<IRepository<Model.PasswordRequest>> passwordRequestRepositoryMock;
        private readonly SessionData sessionData;

        public PasswordRequestValidatorTest()
        {
            this.passwordRequestRepositoryMock = new Mock<IRepository<Model.PasswordRequest>>();
            this.passwordRequestValidator = new PasswordRequestValidator(this.passwordRequestRepositoryMock.Object);
            this.sessionData = new SessionData(new User(), new Model.Mandator());
        }

        [Fact]
        public void CanRequestPasswordShouldThrowAnArgumentNullExceptionIfTheIpParameterIsNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => this.passwordRequestValidator.CanRequestPassword(string.Empty, this.sessionData));
        }

        [Fact]
        public void CanRequestShouldFindTwoPasswordRequestsAndReturnTrue()
        {
            const string myIp = "myIp";

            this.passwordRequestRepositoryMock.Setup(n => n.CreateQueryable(this.sessionData))
                                            .Returns(new List<Model.PasswordRequest>
                                                     {
                                                         new Model.PasswordRequest
                                                         {
                                                             Id = 1,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         },
                                                         new Model.PasswordRequest
                                                         {
                                                             Id = 2,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         }
                                                     }.AsQueryable());

            Assert.True(this.passwordRequestValidator.CanRequestPassword(myIp, this.sessionData));
        }

        [Fact]
        public void CanRequestShouldFindFourPasswordRequestsAndReturnFalse()
        {

            const string myIp = "myIp";

            this.passwordRequestRepositoryMock.Setup(n => n.CreateQueryable(this.sessionData))
                                            .Returns(new List<Model.PasswordRequest>
                                                     {
                                                         new Model.PasswordRequest
                                                         {
                                                             Id = 1,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         },
                                                         new Model.PasswordRequest
                                                         {
                                                             Id = 2,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         },
                                                        new Model.PasswordRequest
                                                         {
                                                             Id = 3,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         },
                                                         new Model.PasswordRequest
                                                         {
                                                             Id = 4,
                                                             IP = myIp,
                                                             TimeStamp = DateTime.Now,
                                                             Username = "myUsername"
                                                         }
                                                     }.AsQueryable());

            Assert.False(this.passwordRequestValidator.CanRequestPassword(myIp, this.sessionData));
        }
    }
}
