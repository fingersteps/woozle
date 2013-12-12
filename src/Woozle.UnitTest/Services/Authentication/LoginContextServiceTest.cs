using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Authentication;

namespace Woozle.UnitTest.Services.Authentication
{
    [TestClass]
    public class LoginContextServiceTest : SessionTestBase
    {
        private LoginContextService loginContextService;
        private User user;
        private Model.Mandator mandator;

        [TestInitialize]
        public void Initialize()
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

        [TestMethod]
        public void GetLoginContextOfLoggedInUserTest()
        {
            user.Username = "sha";
            mandator.Name = "mandator1";

            var result = loginContextService.Get(new LoginContext());

            Assert.IsNotNull(result.UserDto);
            Assert.AreEqual(user.Username, result.UserDto.Username);
            Assert.IsNotNull(result.MandatorDto);
            Assert.AreEqual(mandator.Name, result.MandatorDto.Name);
        }
    }
}
