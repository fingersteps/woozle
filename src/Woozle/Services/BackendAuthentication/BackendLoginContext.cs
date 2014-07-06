using ServiceStack.ServiceHost;
using Woozle.Services.UserManagement;

namespace Woozle.Services.BackendAuthentication
{
    [Route("/backendLoginContext", "GET")]
    public class BackendLoginContext : IReturn<BackendLoginContextResult>
    {
    }

    public class BackendLoginContextResult
    {
        public User User { get; set; }
        public Mandator.Mandator Mandator { get; set; }
    }
}
