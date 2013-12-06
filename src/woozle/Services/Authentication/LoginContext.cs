using ServiceStack;
using Woozle.Core.Services.Stack.ServiceModel.UserManagement;

namespace Woozle.Core.Services.Stack.ServiceModel.LoginContext
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
