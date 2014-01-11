
using System;

namespace Woozle.Services.Authority
{
    [Serializable]
    public partial class Role : WoozleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogicalId { get; set; }
    }
}
