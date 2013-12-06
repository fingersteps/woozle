using System;

namespace Woozle.Core.Model.ModulePermissions
{
    public class ModulePermissionsResult
    {

        private bool hasPermission;

        public int FunctionPermissionId { get; set; }

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string FunctionName { get; set; }

        public string PermissionName { get; set; }

        public bool HasPermission
        {
            get { return this.hasPermission; }
            set
            {
                bool oldValue = this.hasPermission;
                this.hasPermission = value;
            }
        }

    }
}
