using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Location
{
    public interface ILocationLogic
    {
        IList<City> GetCities(SessionData sessionData);
        IList<Country> GetCountries(SessionData sessionData);
        IList<Language> GetLanguages(SessionData sessionData);
    }
}
