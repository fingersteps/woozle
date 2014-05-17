using System.Collections.Generic;
using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Location;

namespace Woozle.Services.Location
{
    [Authenticate]
    public class LanguageService : AbstractService
    {
        private readonly ILocationLogic locationLogic;

        public LanguageService(ILocationLogic locationLogic)
        {
            this.locationLogic = locationLogic;
        }

        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<Language> Get(Languages request)
        {
            var result = locationLogic.GetLanguages(Session.SessionData);
            return Mapper.Map<IEnumerable<Woozle.Model.Language>, List<Language>>(result);
        }
    }
}
