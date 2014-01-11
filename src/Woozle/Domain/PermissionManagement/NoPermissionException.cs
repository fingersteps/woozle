using System;

namespace Woozle.Domain.PermissionManagement
{
    public class NoPermissionException : SystemException
    {
        public NoPermissionException(string logicalFunctionId, string permissionId)
            : base(string.Format("No permission for permission '{0}' in function '{1}'", permissionId, logicalFunctionId))
        {
        }
    }
}
