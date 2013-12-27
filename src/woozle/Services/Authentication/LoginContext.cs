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
        public User User { get; set; }
        public Mandator.Mandator Mandator { get; set; }
    }
}
