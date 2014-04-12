using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Location
{
    public class LocationLogic : ILocationLogic
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
        public IList<City> GetCities(SessionData sessionData)
        {
            return this.cityRepository.FindAll(sessionData).ToList();
        }

        /// <summary>
        /// <see cref="ILocationLogic.GetCountries"/>
        /// </summary>
        public IList<Country> GetCountries(SessionData sessionData)
        {
            return this.countryRepository.FindAll(sessionData).ToList();
        }

        /// <summary>
        /// <see cref="ILocationLogic.GetLanguages"/>
        /// </summary>
        public IList<Language> GetLanguages(SessionData sessionData)
        {
            return this.languageRepository.FindAll(sessionData).ToList();
        }

        public Language LoadLanguage(string languageCode)
        {
            var systemSessionData = new SessionData(null, null);
            return languageRepository.FindByExp(n => n.Code == languageCode, systemSessionData).First();
        }

        #endregion
    }
}
