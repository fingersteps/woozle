using System;
using ServiceStack.ServiceHost;

namespace Woozle.Services.userProfile
{
    [Serializable]
    [Route("/myProfile/changePassword", "PUT")]
    public class ChangeMyPasswordData
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
