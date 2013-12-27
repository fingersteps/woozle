using System;
using Woozle.Services.Authority;

namespace Woozle.Services.UserManagement
{
    [Serializable]
    public partial class UserMandatorRole : WoozleDto
    {
        public int UserId { get; set; }
        public int MandatorRoleId { get; set; }
    
        public MandatorRole MandatorRole { get; set; }
        public User User { get; set; }
    }
    
}
