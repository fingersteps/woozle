using System;
using Woozle.Services;
using Woozle.Services.Authority;
using Woozle.Services.UserManagement;

namespace Woozle.Service.UserManagement
{
    [Serializable]
    public partial class UserMandatorRoleDto : WoozleDto
    {
        public int UserId { get; set; }
        public int MandatorRoleId { get; set; }
    
        public MandatorRole MandatorRole { get; set; }
        public UserDto UserDto { get; set; }
    }
    
}
