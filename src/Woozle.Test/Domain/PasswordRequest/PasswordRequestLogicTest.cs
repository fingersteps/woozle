using System;
using Moq;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.PasswordRequest;
using Woozle.Domain.UserManagement;
using Woozle.ExternalSystem;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Xunit;

namespace Woozle.Test.Domain.PasswordRequest
{
    public class PasswordRequestLogicTest
    {
        private readonly PasswordRequestLogic logic;
        private readonly Mock<IUserLogic> userLogicMock;
        private readonly Mock<IExternalSystemFacadeFactory> externalSystemFacadeFactoryMock;
        private readonly Mock<IExternalSystemFacade<IExternalEMailSystem>> externalEmailSystemFacadeMock;

        public PasswordRequestLogicTest()
        {
            this.userLogicMock = new Mock<IUserLogic>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.externalEmailSystemFacadeMock = new Mock<IExternalSystemFacade<IExternalEMailSystem>>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.logic = new PasswordRequestLogic(this.userLogicMock.Object, this.externalSystemFacadeFactoryMock.Object);
        }

        [Fact]
        public void EmptyUsernameShouldThrowAnArgumentExceptionAcceptedOnANewPasswordRequest()
        {
            var sessionData = new SessionData(new User(), new Model.Mandator());

            Assert.Throws<ArgumentNullException>(() => this.logic.RequestNewPassword(String.Empty, sessionData));
        }

        [Fact]
        public void UsernameRequestShouldGetTheMailAdressOfTheSpecifiedUser()
        {
            const string username = "myUsername";
            const string expectedEmailAdress = "my@email.com";
            var sessionData = new SessionData(new User(), new Model.Mandator());
           
            var user = new User
                       {
                           Username = username,
                           Email = expectedEmailAdress
                       };


            this.userLogicMock.Setup(n => n.GetUserByUsername(username, sessionData))
                              .Returns(user);

            this.logic.RequestNewPassword(username, sessionData);

            this.userLogicMock.Verify(n => n.GetUserByUsername(username, sessionData), Times.Once);
        }

        [Fact]
        public void UsernameRequestWithAnInvalidUserShouldThrowAnArgumentException()
        {
            const string username = "myUsername";
            var sessionData = new SessionData(new User(), new Model.Mandator());


            Assert.Throws<ArgumentException>((() => (this.logic.RequestNewPassword(username, sessionData))));
        }

        [Fact]
        public void ShouldThrowASystemExceptionIfTheExternalSystemWasnotFound()
        {
            const string userName = "myUsername";
            var sessionData = new SessionData(new User(), new Model.Mandator());

            this.externalSystemFacadeFactoryMock.Setup(n => n.GetExternalSystemFacade<IExternalEMailSystem>())
                                                .Returns(externalEmailSystemFacadeMock.Object);

            this.userLogicMock.Setup(n => n.GetUserByUsername(userName, sessionData))
                                .Returns(new User
                                         {
                                             Email = "myEmailAdress"
                                         });

            Assert.Throws<SystemException>(() => (this.logic.RequestNewPassword(userName, sessionData)));
        }

            
    }
}
