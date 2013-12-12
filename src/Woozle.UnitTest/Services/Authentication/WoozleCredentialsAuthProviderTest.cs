using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;

namespace Woozle.UnitTest.Services.Authentication
{
    [TestClass]
    public class WoozleCredentialsAuthProviderTest
    {
        private WoozleCredentialsAuthProvider prosaCredentialsAuthProvider;
        private Mock<IAuthenticationLogic> authenticationLogicMock;
        private Mock<IServiceBase> servicebaseMock;

        [TestInitialize]
        public void Initialize()
        {
            authenticationLogicMock = new Mock<IAuthenticationLogic>();
            servicebaseMock = new Mock<IServiceBase>();
            prosaCredentialsAuthProvider = new WoozleCredentialsAuthProvider(authenticationLogicMock.Object);
        }

        [TestMethod]
        public void TryAuthenticateLoginSuccessfulWithOneMandatorTest()
        {
            const string username = "pro";
            const string password = "test";
            const string mandatorName = "test";

            var user = new User
                           {
                               Username = username
                           };
            var mandator = new Model.Mandator
                               {
                                   Name = mandatorName
                               };


            var sessionData = new SessionData(user, mandator);
            

            var result = new LoginResult(sessionData, true);

 
            authenticationLogicMock.Setup(n => n.Login(It.IsAny<LoginRequest>()))
                                   .Returns(result);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.IsTrue(authenticateResult);
        }

        [TestMethod]
        public void TryAuthenticateLoginSuccessfulWithMoreThanOneMandatorTest()
        {
            const string username = "pro";
            const string password = "test";

            var mandator1 = new Model.Mandator
                                {
                                    Name = "m1"
                                };

            var mandator2 = new Model.Mandator
            {
                Name = "m2"
            };


            var mandator3 = new Model.Mandator
            {
                Name = "m3"
            };

            var mandator4 = new Model.Mandator
            {
                Name = "m4"
            };


            var result = new LoginResult(null, false, true, new[]
                                                                {
                                                                    mandator1,
                                                                    mandator2,
                                                                    mandator3,
                                                                    mandator4
                                                                });


            authenticationLogicMock.Setup(n => n.Login(It.IsAny<LoginRequest>()))
                                   .Returns(result);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.IsTrue(authenticateResult);
        }

        [TestMethod]
        public void TryAuthenticateLoginNotSuccessfulTest()
        {
            const string username = "pro";
            const string password = "test";

            var result = new LoginResult(null, false);


            authenticationLogicMock.Setup(n => n.Login(It.IsAny<LoginRequest>()))
                                   .Returns(result);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.IsFalse(authenticateResult);
        }
    }
}
