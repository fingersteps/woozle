using System;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authentication;
using Xunit;

namespace Woozle.UnitTest.Services.Authentication
{
    public class LoginContextServiceTest : SessionTestBase
    {
        private readonly LoginContextService loginContextService;
        private readonly User user;
        private readonly Model.Mandator mandator;

        public LoginContextServiceTest()
        {
            MappingConfiguration.Configure();

            user = new User();
            mandator = new Model.Mandator();
            var sessionData = new SessionData(user, mandator);
            var session = new Session(Guid.NewGuid(), sessionData, DateTime.Now);

            var requestContextMock = this.GetRequestContextMock(session);

            loginContextService = new LoginContextService()
                {
                    RequestContext = requestContextMock
                };
        }

        [Fact]
        public void GetLoginContextOfLoggedInUserTest()
        {
            user.Username = "sha";
            mandator.Name = "mandator1";

            var result = loginContextService.Get(new LoginContext());

            Assert.NotNull(result.UserDto);
            Assert.Equal(user.Username, result.UserDto.Username);
            Assert.NotNull(result.MandatorDto);
            Assert.Equal(mandator.Name, result.MandatorDto.Name);
        }
    }
}
