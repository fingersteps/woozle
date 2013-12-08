using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Persistence.Repository;

namespace Woozle.Domain.Settings
{
    public class SettingsLogic : ISettingsLogic
    {
        private readonly IRepository<Setting> settingsRepository;

        public SettingsLogic(IRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public Setting Load(Session session)
        {
            var mandatorSettings = settingsRepository.CreateQueryable(session).FirstOrDefault();
            return mandatorSettings;
        }

        public ISaveResult<Setting> Save(Setting saveableObject, Session session)
        {
            var savedSettings = settingsRepository.Save(saveableObject, session);
            settingsRepository.UnitOfWork.Commit();
            return new SaveResult<Setting>() {TargetObject = savedSettings};
        }
    }
}
