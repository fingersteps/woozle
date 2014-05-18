using System;
using Moq;
using Woozle.Domain.ExternalSystem;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.ExternalSystem.Mail;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.PasswordRequest;
using Woozle.Domain.UserManagement;
using Woozle.Domain.UserProfile;
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
        private readonly Mock<IExternalSystemFacade<IExternalMailSystem>> externalEmailSystemFacadeMock;
        private readonly Mock<IExternalMailSystem> externalEmailSystem;
        private readonly Mock<IPasswordChangeLogic> passwordChangeLogicMock;

        public PasswordRequestLogicTest()
        {
            this.userLogicMock = new Mock<IUserLogic>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.externalEmailSystemFacadeMock = new Mock<IExternalSystemFacade<IExternalMailSystem>>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.passwordChangeLogicMock = new Mock<IPasswordChangeLogic>();
            this.externalEmailSystem = new Mock<IExternalMailSystem>();
            //this.logic = new PasswordRequestLogic(
            //    this.userLogicMock.Object,
            //    this.externalSystemFacadeFactoryMock.Object,
            //    this.passwordChangeLogicMock.Object);
        }

        [Fact]
        public void EmptyUsernameShouldThrowAnArgumentExceptionAcceptedOnANewPasswordRequest()
        {
            var sessionData = new SessionData(new User(), new Model.Mandator());

          //  Assert.Throws<ArgumentNullException>(() => this.logic.RequestNewPassword(String.Empty, String.Empty, String.Empty, sessionData));
        }

        [Fact]
        public void UsernameRequestShouldGetTheMailAdressOfTheSpecifiedUser()
        {
            const string username = "myUsername";
            const string expectedEmailAdress = "my@email.com";
            var sessionData = new SessionData(new User(), new Model.Mandator());

            this.externalSystemFacadeFactoryMock.Setup(n => n.GetExternalSystemFacade<IExternalMailSystem>())
                                                .Returns(this.externalEmailSystemFacadeMock.Object);

            this.externalEmailSystemFacadeMock.Setup(n => n.GetExternalSystem(sessionData))
                                              .Returns(this.externalEmailSystem.Object);
           
            var user = new User
                       {
                           Username = username,
                           Email = expectedEmailAdress
                       };


            this.userLogicMock.Setup(n => n.GetUserByUsername(username, sessionData))
                              .Returns(user);

           // this.logic.RequestNewPassword(username, string.Empty, "my text", sessionData);

            this.userLogicMock.Verify(n => n.GetUserByUsername(username, sessionData), Times.Once);
        }

        [Fact]
        public void UsernameRequestWithAnInvalidUserShouldThrowAnArgumentException()
        {
            const string username = "myUsername";
            var sessionData = new SessionData(new User(), new Model.Mandator());

        //    Assert.Throws<ArgumentNullException>((() => (this.logic.RequestNewPassword(username, string.Empty, string.Empty, sessionData))));
        }

        [Fact]
        public void ShouldThrowASystemExceptionIfTheExternalSystemWasnotFound()
        {
            const string userName = "myUsername";
            var sessionData = new SessionData(new User(), new Model.Mandator());

            this.externalSystemFacadeFactoryMock.Setup(n => n.GetExternalSystemFacade<IExternalMailSystem>())
                                                .Returns(externalEmailSystemFacadeMock.Object);

            this.userLogicMock.Setup(n => n.GetUserByUsername(userName, sessionData))
                                .Returns(new User
                                         {
                                             Email = "myEmailAdress"
                                         });

      //      Assert.Throws<SystemException>(() => (this.logic.RequestNewPassword(userName, string.Empty, "my text", sessionData)));
        }
    }
}
