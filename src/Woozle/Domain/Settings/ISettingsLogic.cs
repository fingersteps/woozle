using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;

namespace Woozle.Domain.Settings
{
    /// <summary>
    /// Settings related logic
    /// </summary>
    public interface ISettingsLogic
    {
        /// <summary>
        /// Loads a specific <see cref="Setting"/>
        /// </summary>
        /// <param name="sessionData">The session</param>
        /// <returns>The loaded <see cref="Setting"/></returns>
        Setting Load(SessionData sessionData);

        /// <summary>
        /// Saves a specific <see cref="Setting"/>
        /// </summary>
        /// <param name="saveableObject">The <see cref="Setting"/> to save.</param>
        /// <param name="sessionData"><see cref="Session"/></param>
        /// <returns>The result of the save process as <see cref="ISaveResult{T}"/></returns>
        ISaveResult<Setting> Save(Setting saveableObject, SessionData sessionData);
    }
}
