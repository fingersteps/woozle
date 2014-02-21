using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.ModuleManagement
{
    /// <summary>
    /// Definition of the module management functionalities.
    /// </summary>
    public interface IModuleLogic
    {
        /// <summary>
        /// Gets all modules by given mandator
        /// </summary>
        /// <param name="sessionData">The session</param>
        /// <returns>All Modules of the mandator</returns>
        /// 
        IList<ModuleForMandator> GetModulesByMandator(SessionData sessionData);

        /// <summary>
        /// Gets all permissions of modules/functions acc. the mandant of the given session.
        /// </summary>
        /// <param name="role"> </param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        IList<ModulePermissionsResult> FindModulePermissions(Role role, SessionData sessionData);
    }
}
