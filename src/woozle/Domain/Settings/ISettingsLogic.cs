using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Model.Validation.Creation;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Settings
{
    public interface ISettingsLogic
    {
        Setting Load(Session session);
        ISaveResult<Setting> Save(Setting saveableObject, Session session);
    }
}
