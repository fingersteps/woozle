using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    public class ModulePermissionsResult
    {
        public int FunctionPermissionId { get; set; }

        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string FunctionName { get; set; }

        public string PermissionName { get; set; }

        public bool HasPermission { get; set; }
    }
}
