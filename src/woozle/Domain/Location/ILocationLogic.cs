using System.Collections.Generic;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Cities
{
    public interface ILocationLogic
    {
        IList<City> GetCities(Session session);
        IList<Country> GetCountries(Session session);
        IList<Language> GetLanguages(Session session);
    }
}
