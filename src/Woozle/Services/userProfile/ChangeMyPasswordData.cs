using System;
using ServiceStack.ServiceHost;

namespace Woozle.Services.UserProfile
{
    [Serializable]
    [Route("/myProfile/changePassword", "PUT")]
    public class ChangeMyPasswordData
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
