using ServiceStack.ServiceHost;

namespace Woozle.Services.Location
{
    [Route("/selectUserLanguage", "POST")]
    public class SelectUserLanguage
    {
        public string LanguageCode { get; set; }
    }
}
