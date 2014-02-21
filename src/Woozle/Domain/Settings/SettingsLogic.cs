using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Persistence;

namespace Woozle.Domain.Settings
{
    public class SettingsLogic : ISettingsLogic
    {
        private readonly IRepository<Setting> settingsRepository;

        public SettingsLogic(IRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public Setting Load(SessionData sessionData)
        {
            var mandatorSettings = settingsRepository.CreateQueryable(sessionData).FirstOrDefault();
            return mandatorSettings;
        }

        public ISaveResult<Setting> Save(Setting saveableObject, SessionData sessionData)
        {
            var savedSettings = settingsRepository.Save(saveableObject, sessionData);
            settingsRepository.UnitOfWork.Commit();
            return new SaveResult<Setting>() {TargetObject = savedSettings};
        }
    }
}
