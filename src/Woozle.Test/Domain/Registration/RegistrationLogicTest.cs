using System;
using Moq;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.Location;
using Woozle.Domain.Registration;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authority;
using Woozle.Settings;
using Xunit;

namespace Woozle.Test.Domain.Registration
{
    public class RegistrationLogicTest
    {
        private Mock<IUserLogic> userLogicMock;
        private Mock<IWoozleSettings> woozleSettingsMock;
        private Mock<IRegistrationSettings> registrationSettingsMock;
        private Mock<IGetRolesLogic> getRolesLogicMock;
        private Mock<IHashProvider> passwordHasherMock;
        private RegistrationLogic registrationLogic;
        private Mock<IUserValidator> userValidatorMock;
        private Mock<ILocationLogic> locationLogicMock;

        public RegistrationLogicTest()
        {
            userLogicMock = new Mock<IUserLogic>();
            woozleSettingsMock = new Mock<IWoozleSettings>();
            registrationSettingsMock = new Mock<IRegistrationSettings>();
            getRolesLogicMock = new Mock<IGetRolesLogic>();
            passwordHasherMock = new Mock<IHashProvider>();
            userValidatorMock = new Mock<IUserValidator>();
            locationLogicMock = new Mock<ILocationLogic>();

            registrationLogic = new RegistrationLogic(userLogicMock.Object, woozleSettingsMock.Object,
                registrationSettingsMock.Object, getRolesLogicMock.Object, passwordHasherMock.Object, userValidatorMock.Object, locationLogicMock.Object);

            MappingConfiguration.Configure();
        }

        [Fact]
        public void CreateUserAuth_MappedFields()
        {
            MockCreateUserDependencies();
            locationLogicMock.Setup(n => n.LoadLanguage("de")).Returns(new Language() {Id = 2});

            var user = new User()
            {
                Username = "user",
                FirstName = "firstname",
                LastName = "lastname",
                Language = new Language() {Code = "de"}
            };

            var registeredUser = registrationLogic.RegisterUser(user, "password");

            Assert.Equal(user.Username, registeredUser.Username);
            Assert.Equal(user.FirstName, registeredUser.FirstName);
            Assert.Equal(user.LastName, registeredUser.LastName);
            Assert.Equal(2, registeredUser.LanguageId);
        }

        [Fact]
        public void CreateUserAuth_SetPassword()
        {
            MockCreateUserDependencies();
            woozleSettingsMock.Setup(n => n.DefaultLanguage).Returns(new Language() {Id = 4});

            var user = new User()
            {
                Username = "user",
                FirstName = "firstname",
                LastName = "lastname",
                Language = new Language()
            };

            string password = "MyPassword";
            string hash = "Hash";
            string salt = "Salt";
            passwordHasherMock.Setup(n => n.GetHashAndSaltString(password, out hash, out salt));

            var registeredUser = registrationLogic.RegisterUser(user, password);
            
            Assert.Equal(hash, registeredUser.PasswordHash);
            Assert.Equal(salt, registeredUser.PasswordSalt);
            Assert.Equal(4, registeredUser.LanguageId);
        }

        [Fact]
        public void CreateUserAuth_ShouldValidateUser()
        {
            MockCreateUserDependencies();
            woozleSettingsMock.Setup(n => n.DefaultLanguage).Returns(new Language() { Id = 4 });

            var user = new User()
            {
                FirstName = "firstname",
                LastName = "lastname",
                Language = new Language()
            };

            userValidatorMock.Setup(n => n.ValidateNewUser(user.Username, user.Email));
            userValidatorMock.Setup(n => n.ValidateUserPassword("password"));

            registrationLogic.RegisterUser(user, "password");
            userLogicMock.Verify();
        }

        [Fact]
        public void CreateUserAuth_UsernameAlreadyExists()
        {
            MockCreateUserDependencies();
            userLogicMock.Setup(n => n.GetUserByUsername("user", It.IsAny<SessionData>())).Returns(new User());

            var user = new User()
            {
                FirstName = "firstname",
                LastName = "lastname",
                Username = "user"
            };

            Assert.Throws<ArgumentException>(() => registrationLogic.RegisterUser(user, "password"));
        }

        private void MockCreateUserDependencies()
        {
            userLogicMock.Setup(n => n.Save(It.IsAny<User>(), It.IsAny<SessionData>())).Returns((User u, SessionData s) => u);
            woozleSettingsMock.Setup(n => n.DefaultMandator).Returns(new Model.Mandator() { Id = 1 });
            registrationSettingsMock.Setup(n => n.DefaultFlagActiveStatus).Returns(new Status() { Id = 3 });
            getRolesLogicMock.Setup(n => n.GetMandatorRoleByName(Roles.User, It.IsAny<SessionData>()))
                .Returns(new Model.MandatorRole() { Id = 4 });
        }
    }
}
