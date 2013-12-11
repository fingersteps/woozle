using ServiceStack.ServiceHost;
using Woozle.Services.UserManagement;

namespace Woozle.Services.Authentication
{
    [Route("/loginContext", "GET")]
    public class LoginContext : IReturn<LoginContextResultDto>
    {
    }

    public class LoginContextResultDto
    {
        public UserDto UserDto { get; set; }
        public Mandator.MandatorDto MandatorDto { get; set; }
    }
}
