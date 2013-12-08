using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Authority
{
    public interface IPermissionsLogic
    {
        /// <summary>
        /// Persists the given changed permissions to database.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="changedPermissions"></param>
        /// <param name="session"></param>
        void SaveChangedPermissions(Role role, List<ChangedModulePermission> changedPermissions, Session session);

        /// <summary>
        /// Gets the assigned <see cref="FunctionPermission"/> for the logged in <see cref="User"/>
        /// </summary>
        /// <param name="session"><see cref="Session"/></param>
        /// <returns>A list of <see cref="FunctionPermission"/></returns>
        IList<FunctionPermission> GetAssignedPermissions(Session session);
    }
}
