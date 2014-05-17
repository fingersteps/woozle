using ServiceStack.ServiceInterface;
using Woozle.Domain.Location;
using Woozle.Model.SessionHandling;
using Woozle.Settings;

namespace Woozle.Services.Location
{
    public class DefaultUserLanguageService : PublicService
    {
        private readonly ILocationLogic languageLogic;

        public DefaultUserLanguageService(ILocationLogic languageLogic, IWoozleSettings woozleSettings)
            : base(woozleSettings)
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
        }
    }
}
