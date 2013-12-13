using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence
{
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// Finds all permissions related to modules/functions for the current mandant (acc. the given session)
        /// </summary>
        /// <param name="role"> </param>
        /// <param name="session"></param>
        /// <returns></returns>
        IList<ModulePermissionsResult> FindModulePermissions(Role role, Session session);

        /// <summary>
        /// Finds modules for a given mandator.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        List<ModuleForMandator> FindModulesForMandator(Session session);
    }
}
