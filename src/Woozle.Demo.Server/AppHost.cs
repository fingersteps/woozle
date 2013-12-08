using Woozle.Host;

namespace Woozle.Demo.Server
{
    public class AppHost : WoozleHost 
    {
        public AppHost() : base("Woozle Testserver", typeof(WoozleHost).Assembly, typeof(TestService).Assembly)
        {
            
        }
    }
}
