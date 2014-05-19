using System;
using Moq;
using ServiceStack.ServiceHost;
using Woozle.Domain.Communication;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Domain.ExternalSystem.Mail;
using Woozle.Domain.PasswordChange;
using Woozle.Domain.PasswordRequest;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
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
        private readonly Mock<ICommunicationProvider> communicationProviderMock;
        private readonly Mock<IPasswordGenerator> passwordGeneratorMock;
        private readonly Mock<IRepository<Model.PasswordRequest>> repositoryMock;
        private readonly Mock<IPasswordRequestValidator> passwordRequestValidatorMock;
        private readonly Mock<IRequestContext> requestContextMock;

        public PasswordRequestLogicTest()
        {
            this.userLogicMock = new Mock<IUserLogic>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.externalEmailSystemFacadeMock = new Mock<IExternalSystemFacade<IExternalMailSystem>>();
            this.externalSystemFacadeFactoryMock = new Mock<IExternalSystemFacadeFactory>();
            this.passwordChangeLogicMock = new Mock<IPasswordChangeLogic>();
            this.externalEmailSystem = new Mock<IExternalMailSystem>();
            this.communicationProviderMock = new Mock<ICommunicationProvider>();
            this.passwordGeneratorMock = new Mock<IPasswordGenerator>();
            this.repositoryMock = new Mock<IRepository<Model.PasswordRequest>>();
            this.passwordRequestValidatorMock = new Mock<IPasswordRequestValidator>();
            this.requestContextMock = new Mock<IRequestContext>();

            this.logic = new PasswordRequestLogic(
                this.userLogicMock.Object,
                this.passwordChangeLogicMock.Object,
                this.communicationProviderMock.Object,
                this.passwordGeneratorMock.Object,
                repositoryMock.Object,
                passwordRequestValidatorMock.Object);
            //this.requestContextMock.Object);
        }

        [Fact]
        public void EmptyUsernameShouldThrowAnArgumentExceptionAcceptedOnANewPasswordRequest()
        {
            var sessionData = new SessionData(new User(), new Model.Mandator());

            Assert.Throws<ArgumentNullException>(() => this.logic.RequestNewPassword(string.Empty, String.Empty, String.Empty, String.Empty, sessionData, (s, s1, arg3) => string.Empty));
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

            this.logic.RequestNewPassword(string.Empty,username, string.Empty, "my text", sessionData, (s, s1, arg3) => string.Empty);

            this.userLogicMock.Verify(n => n.GetUserByUsername(username, sessionData), Times.Once);
        }

        [Fact]
        public void UsernameRequestWithAnInvalidUserShouldThrowAnArgumentException()
        {
            const string username = "myUsername";
            var sessionData = new SessionData(new User(), new Model.Mandator());

            Assert.Throws<ArgumentException>((() => (this.logic.RequestNewPassword(string.Empty, username, string.Empty, string.Empty, sessionData, (s, s1, arg3) => string.Empty))));
        }
    }
}
