using System;
using Moq;
using Woozle.Domain.Communication;
using Woozle.Domain.ExternalSystem;
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
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IPasswordRequestValidator> passwordRequestValidatorMock;

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
            this.unitOfWorkMock = new Mock<IUnitOfWork>();

            this.repositoryMock.Setup(n => n.UnitOfWork)
                                .Returns(this.unitOfWorkMock.Object);

            this.logic = new PasswordRequestLogic(
                this.userLogicMock.Object,
                this.passwordChangeLogicMock.Object,
                this.communicationProviderMock.Object,
                this.passwordGeneratorMock.Object,
                repositoryMock.Object,
                passwordRequestValidatorMock.Object);
        }

        private SessionData GetSessionData()
        {
            return new SessionData(new User(), new Model.Mandator());
        }

        [Fact]
        public void CredentialsShouldReturnTheUsernameOfTheCommunicationProviderCredential()
        {
            const string username = "myUsername";

            this.communicationProviderMock.Setup(n => n.Credentials)
                                            .Returns(new ExternalSystemCredentials
                                                     {
                                                         Username = username
                                                     });



            Assert.Equal(username, this.logic.Credentials.Username);
        }


        [Fact]
        public void CredentialsShouldReturnThePasswordOfTheCommunicationProviderCredential()
        {
            const string password = "myPassword";

            this.communicationProviderMock.Setup(n => n.Credentials)
                                            .Returns(new ExternalSystemCredentials
                                            {
                                                Password = password
                                            });



            Assert.Equal(password, this.logic.Credentials.Password);
        }

        [Fact]
        public void EmptyUsernameShouldThrowAnArgumentExceptionAcceptedOnANewPasswordRequest()
        {
            var sessionData = this.GetSessionData();

            Assert.Throws<ArgumentNullException>(() => this.logic.RequestNewPassword(string.Empty, String.Empty, String.Empty, String.Empty, sessionData, (s, s1, arg3) => string.Empty));
        }

        [Fact]
        public void RequestNewPasswordShouldCheckIfTheCalleeCanRequestForANewPassword()
        {
            const string ip = "myIp";
            var sessionData = this.GetSessionData();

            this.passwordRequestValidatorMock.Setup(n => n.CanRequestPassword(ip, sessionData))
                                             .Returns(false);

            Assert.False(this.logic.RequestNewPassword(ip, "myUsername", "myText", "myTitle", sessionData,
                (s, s1, arg3) => string.Empty));
        }

        [Fact]
        public void UsernameRequestShouldGetTheMailAdressOfTheSpecifiedUser()
        {
            const string username = "myUsername";
            const string expectedEmailAdress = "my@email.com";
            var sessionData = this.GetSessionData();



            this.passwordRequestValidatorMock.Setup(n => n.CanRequestPassword(string.Empty, sessionData))
                                             .Returns(true);

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

            this.passwordRequestValidatorMock.Setup(n => n.CanRequestPassword(string.Empty, sessionData))
                                           .Returns(true);

            Assert.Throws<ArgumentException>((() => (this.logic.RequestNewPassword(string.Empty, username, string.Empty, string.Empty, sessionData, (s, s1, arg3) => string.Empty))));
        }
    }
}
