using System;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class Permission : WoozleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogicalId { get; set; }
    }
}
