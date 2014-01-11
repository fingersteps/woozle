using System;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class FunctionPermission : WoozleDto
    {
        public int FunctionId { get; set; }
        public int PermissionId { get; set; }
    
        public Function Function { get; set; }
        public Permission Permission { get; set; }
    }
    
}
