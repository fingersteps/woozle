using System;
using ServiceStack.ServiceHost;

namespace Woozle.Services.userProfile
{
    [Serializable]
    [Route("/myProfile", "PUT")]
    public class MyProfileData
    {
        public string Email { get; set; }
        public int LanguageId { get; set; }
    }
}
