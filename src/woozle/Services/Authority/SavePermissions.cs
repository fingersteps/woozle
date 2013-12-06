using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    [Route("/permissions", "PUT, OPTIONS")]
    public class SavePermissions : IReturnVoid
    {
        public Role Role { get; set; }
        public List<ChangedModulePermission> ChangedPermissions { get; set; }
    }
}
