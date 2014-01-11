using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Location
{
    public class LocationLogic : AbstractLogic, ILocationLogic
    {
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<Language> languageRepository;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="cityRepository"></param>
        /// <param name="countryRepository"></param>
        /// <param name="languageRepository"></param>
        public LocationLogic(IRepository<City> cityRepository, IRepository<Country> countryRepository, IRepository<Language> languageRepository)
        {
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
            this.languageRepository = languageRepository;
        }

        #region ILocationLogic Members

        /// <summary>
        /// <see cref="ILocationLogic.GetCities"/>
        /// </summary>
        public IList<City> GetCities(Session session)
        {
            return this.cityRepository.FindAll(session).ToList();
        }

        /// <summary>
        /// <see cref="ILocationLogic.GetCountries"/>
        /// </summary>
        public IList<Country> GetCountries(Session session)
        {
            return this.countryRepository.FindAll(session).ToList();
        }

        /// <summary>
        /// <see cref="ILocationLogic.GetLanguages"/>
        /// </summary>
        public IList<Language> GetLanguages(Session session)
        {
            return this.languageRepository.FindAll(session).ToList();
        }

        #endregion
    }
}
