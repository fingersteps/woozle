using System.Linq;
using Woozle.Core.BusinessLogic.Settings;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Model.Validation.Creation;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.Settings
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
