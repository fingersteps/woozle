using Woozle.Core.Model.SessionHandling;

namespace Woozle.Core.BusinessLogic.PermissionManagement
{
    /// <summary>
    /// Interface for an permissionmanager.
    /// </summary>
    public interface IPermissionManager
    {
        /// <summary>
        /// Returns true, if the user of the <see cref="Session"/> has the specified permission.
        /// </summary>
        /// <param name="session"><see cref="Session"/></param>
        /// <param name="functionLogicalId">The logical id of the function</param>
        /// <param name="permissionId">The logical id of the permission</param>
        /// <returns>True, if the user of the <see cref="Session"/> has the specified permission</returns>
        bool HasPermission(SessionData session, string functionLogicalId, string permissionId);
    }
}
