using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;

namespace Woozle.Domain.Settings
{
    public interface ISettingsLogic
    {
        Setting Load(Session session);
        ISaveResult<Setting> Save(Setting saveableObject, Session session);
    }
}
