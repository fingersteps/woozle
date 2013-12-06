using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    [Route("/role/{Id}/modulePermissions", "GET, OPTIONS")]
    public class RoleModulePermissions : IReturn<List<ModulePermissionsResult>>
    {
        public int Id { get; set; }

        protected bool Equals(RoleModulePermissions other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RoleModulePermissions) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
