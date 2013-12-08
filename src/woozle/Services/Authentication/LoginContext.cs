using ServiceStack;
using ServiceStack.ServiceHost;
using Woozle.Services.UserManagement;

namespace Woozle.Services.Authentication
{
    [Route("/loginContext", "GET")]
    public class LoginContext : IReturn<LoginContextResult>
    {
    }

    public class LoginContextResult
    {
        public UserDto UserDto { get; set; }
        public Mandator.Mandator Mandator { get; set; }
    }
}
