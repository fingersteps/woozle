using System;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class FunctionPermissionDto : WoozleDto
    {
        public int FunctionId { get; set; }
        public int PermissionId { get; set; }
    
        public FunctionDto FunctionDto { get; set; }
        public PermissionDto PermissionDto { get; set; }
    }
    
}
