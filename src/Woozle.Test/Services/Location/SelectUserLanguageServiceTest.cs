using Moq;
using Woozle.Domain.Location;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.Location;
using Xunit;
using Language = Woozle.Model.Language;

namespace Woozle.Test.Services.Location
{
    public class SelectUserLanguageServiceTest : SessionTestBase
    {
        private readonly SelectUserLanguageService setUserDefaultsService;
        private readonly User user;
        private readonly Model.Mandator mandator;
        private readonly Mock<ILocationLogic> locationLogicMock;

        public SelectUserLanguageServiceTest()
        {
            MappingConfiguration.Configure();

            user = new User();
            mandator = new Model.Mandator();
            var sessionData = new SessionData(user, mandator);
            var session = new Session(sessionData);

            var requestContextMock = this.GetFakeRequestContext(session);

            locationLogicMock = new Mock<ILocationLogic>();

            setUserDefaultsService = new SelectUserLanguageService(locationLogicMock.Object)
                {
                    RequestContext = requestContextMock
                };
        }

        [Fact]
        public void SelectUserLanguageEnglishTest()
        {
            const string userLang = "en";
            locationLogicMock.Setup(n => n.LoadLanguage(userLang)).Returns(new Language() { Code = userLang });
            setUserDefaultsService.Post(new SelectUserLanguage() { LanguageCode = userLang });
            Assert.Equal(userLang, user.Language.Code);
        }

        [Fact]
        public void SelectUserLanguageGermanTest()
        {
            const string userLang = "de";
            locationLogicMock.Setup(n => n.LoadLanguage(userLang)).Returns(new Language() { Code = userLang });
            setUserDefaultsService.Post(new SelectUserLanguage() { LanguageCode = userLang });
            Assert.Equal(userLang, user.Language.Code);
        }
    }
}
