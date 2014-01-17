using Funq;
using Moq;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;
using Xunit;

namespace Woozle.Test.Services.Authentication
{
    public class WoozleCredentialsAuthProviderTest
    {
        private readonly WoozleCredentialsAuthProvider prosaCredentialsAuthProvider;
        private readonly Mock<IAuthenticationLogic> authenticationLogicMock;
        private readonly Mock<IServiceBase> servicebaseMock;

        private readonly Container container;

        public WoozleCredentialsAuthProviderTest()
        {
            authenticationLogicMock = new Mock<IAuthenticationLogic>();
            container = new Container();
            
            authenticationLogicMock = new Mock<IAuthenticationLogic>();
            servicebaseMock = new Mock<IServiceBase>();
            prosaCredentialsAuthProvider = new WoozleCredentialsAuthProvider(container);
        }

        [Fact]
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

            container.Register(authenticationLogicMock.Object);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.True(authenticateResult);
        }

        [Fact]
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

            container.Register(authenticationLogicMock.Object);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.True(authenticateResult);
        }

        [Fact]
        public void TryAuthenticateLoginNotSuccessfulTest()
        {
            const string username = "pro";
            const string password = "test";

            var result = new LoginResult(null, false);


            authenticationLogicMock.Setup(n => n.Login(It.IsAny<LoginRequest>()))
                                   .Returns(result);

            container.Register(authenticationLogicMock.Object);

            var authenticateResult = this.prosaCredentialsAuthProvider.TryAuthenticate(servicebaseMock.Object, username, password);

            Assert.False(authenticateResult);
        }
    }
}
