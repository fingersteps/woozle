using System;

namespace Woozle.Services.Authority
{
    [Serializable]
    public partial class MandatorRole : WoozleDto
    {
    
        public int MandId { get; set; }
        public int RoleId { get; set; }
    
        public Mandator.MandatorDto MandatorDto { get; set; }
        public Role Role { get; set; }
    
    }
    
}
