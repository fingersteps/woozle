using System;
using Moq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authentication;
using Woozle.Services.Authority;
using Woozle.Settings;
using Xunit;

namespace Woozle.Test.Services.Authentication
{
    public class WoozleAuthRepositoryTest
    {
        private Mock<IUserLogic> userLogicMock;
        private Mock<IWoozleSettings> woozleSettingsMock;
        private Mock<IRegistrationSettings> registrationSettingsMock;
        private Mock<IGetRolesLogic> getRolesLogicMock;
        private Mock<IHashProvider> passwordHasherMock;
        private WoozleAuthRepository authRepository;
        private Mock<IUserValidator> userValidatorMock;

        public WoozleAuthRepositoryTest()
        {
            userLogicMock = new Mock<IUserLogic>();
            woozleSettingsMock = new Mock<IWoozleSettings>();
            registrationSettingsMock = new Mock<IRegistrationSettings>();
            getRolesLogicMock = new Mock<IGetRolesLogic>();
            passwordHasherMock = new Mock<IHashProvider>();
            userValidatorMock = new Mock<IUserValidator>();

            authRepository = new WoozleAuthRepository(userLogicMock.Object, woozleSettingsMock.Object,
                registrationSettingsMock.Object, getRolesLogicMock.Object, passwordHasherMock.Object, userValidatorMock.Object);

            MappingConfiguration.Configure();
        }

        [Fact]
        public void CreateUserAuth_MappedFields()
        {
            MockCreateUserDependencies();

            var user = new UserAuth()
            {
                UserName = "user",
                FirstName = "firstname",
                LastName = "lastname",
            };
            var registeredUser = authRepository.CreateUserAuth(user, "password");
            
            Assert.Equal(user.UserName, registeredUser.UserName);
            Assert.Equal(user.FirstName, registeredUser.FirstName);
            Assert.Equal(user.LastName, registeredUser.LastName);
        }

        [Fact]
        public void CreateUserAuth_SetPassword()
        {
            MockCreateUserDependencies();

            var user = new UserAuth()
            {
                UserName = "user",
                FirstName = "firstname",
                LastName = "lastname",
            };

            string password = "MyPassword";
            string hash = "Hash";
            string salt = "Salt";
            passwordHasherMock.Setup(n => n.GetHashAndSaltString(password, out hash, out salt));

            var registeredUser = authRepository.CreateUserAuth(user, password);
            
            Assert.Equal(hash, registeredUser.PasswordHash);
            Assert.Equal(salt, registeredUser.Salt);
        }

        [Fact]
        public void CreateUserAuth_ShouldValidateUser()
        {
            MockCreateUserDependencies();

            var user = new UserAuth()
            {
                FirstName = "firstname",
                LastName = "lastname",
            };

            userValidatorMock.Setup(n => n.ValidateNewUser(user.UserName, user.Email));
            userValidatorMock.Setup(n => n.ValidateUserPassword("password"));

            authRepository.CreateUserAuth(user, "password");
            userLogicMock.Verify();
        }

        [Fact]
        public void CreateUserAuth_UsernameAlreadyExists()
        {
            MockCreateUserDependencies();
            userLogicMock.Setup(n => n.GetUserByUsername("user", It.IsAny<SessionData>())).Returns(new User());

            var user = new UserAuth()
            {
                FirstName = "firstname",
                LastName = "lastname",
                UserName = "user"
            };

            Assert.Throws<ArgumentException>(() => authRepository.CreateUserAuth(user, "password"));
        }

        private void MockCreateUserDependencies()
        {
            userLogicMock.Setup(n => n.Save(It.IsAny<User>(), It.IsAny<SessionData>())).Returns(new User());
            woozleSettingsMock.Setup(n => n.DefaultMandator).Returns(new Model.Mandator() { Id = 1 });
            registrationSettingsMock.Setup(n => n.DefaultLanguage).Returns(new Language() { Id = 2 });
            registrationSettingsMock.Setup(n => n.DefaultFlagActiveStatus).Returns(new Status() { Id = 3 });
            getRolesLogicMock.Setup(n => n.GetMandatorRoleByName(Roles.User, It.IsAny<SessionData>()))
                .Returns(new Model.MandatorRole() { Id = 4 });
        }
    }
}
