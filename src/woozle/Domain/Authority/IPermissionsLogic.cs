using System.Collections.Generic;
using Woozle.Core.Model;
using Woozle.Core.Model.ModulePermissions;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Authority
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
