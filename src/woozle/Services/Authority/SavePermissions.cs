using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Authority
{
    [Route("/permissions", "PUT, OPTIONS")]
    public class SavePermissions : IReturnVoid
    {
        public Role Role { get; set; }
        public List<ChangedModulePermission> ChangedPermissions { get; set; }
    }
}
