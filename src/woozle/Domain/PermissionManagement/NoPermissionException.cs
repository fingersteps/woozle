using System;

namespace Woozle.Core.BusinessLogic.PermissionManagement
{
    public class NoPermissionException : SystemException
    {
        private readonly string logicalFunctionId = string.Empty;
        private readonly string permissionId = string.Empty;

        public NoPermissionException(string logicalFunctionId, string permissionId)
            : base(string.Format("No permission for permission '{0}' in function '{1}'", permissionId, logicalFunctionId))
        {
            this.logicalFunctionId = logicalFunctionId;
            this.permissionId = permissionId;
        }
    }
}
