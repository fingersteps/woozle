using System;
using ServiceStack.ServiceHost;

namespace Woozle.Services.PasswordRequest
{
    [Serializable]
    [Route("/myProfile/requestNewPassword", "POST")]
    public class ResetMyPassword
    {
        public string Username { get; set; }
    }
}
