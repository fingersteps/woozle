using System.Collections.Generic;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.PermissionManagement
{
    /// <summary>
    /// Dataprovider for permissions.
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// Gets all permission of the user of a <see cref="Session"/>.
        /// </summary>
        /// <param name="session">The current <see cref="SessionData"/></param>
        /// <returns>A list of <see cref="FunctionPermission"/> of the <see cref="User"/> from the <see cref="Session"/></returns>
        IList<FunctionPermission> GetAssignedPermissions(SessionData session);
    }
}
