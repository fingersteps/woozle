using System.Linq;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PermissionManagement
{
    public class PermissionManager : IPermissionManager
    {
        /// <summary>
        /// <see cref="IPermissionProvider"/>
        /// </summary>
        private readonly IPermissionProvider permissionProvider;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="provider"><see cref="IPermissionProvider"/></param>
        public PermissionManager(IPermissionProvider provider)
        {
            this.permissionProvider = provider;
        }

        /// <summary>
        /// <see cref="IPermissionManager.HasPermission"/>
        /// </summary>
        public bool HasPermission(SessionData sessionData, string functionLogicalId, string permissionId)
        {
            var permissions = this.permissionProvider.GetAssignedPermissions(sessionData);

            if (permissions == null) return false;

            return permissions.Count(n => n.Permission.LogicalId == permissionId
                                                   && n.Function.LogicalId == functionLogicalId) > 0;
        }
    }
}
