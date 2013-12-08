using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Location
{
    public interface ILocationLogic
    {
        IList<City> GetCities(Session session);
        IList<Country> GetCountries(Session session);
        IList<Language> GetLanguages(Session session);
    }
}
