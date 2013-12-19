using System;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class PermissionDto : WoozleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogicalId { get; set; }
    }
}
