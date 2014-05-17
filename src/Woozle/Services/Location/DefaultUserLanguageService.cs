using ServiceStack.ServiceInterface;
using Woozle.Domain.Location;
using Woozle.Model.SessionHandling;

namespace Woozle.Services.Location
{
    public class DefaultUserLanguageService : AbstractService
    {
        private readonly ILocationLogic languageLogic;

        public DefaultUserLanguageService(ILocationLogic languageLogic)
        {
            this.languageLogic = languageLogic;
        }

        [ExceptionCatcher]
        public void Post(SelectUserLanguage selectUserLanguage)
        {
            var session = (Session)base.Request.GetSession();
            var language = languageLogic.LoadLanguage(selectUserLanguage.LanguageCode);
            session.SessionData.User.LanguageId = language.Id;
            session.SessionData.User.Language = language;
            this.SaveSession(session);
        }
    }
}
