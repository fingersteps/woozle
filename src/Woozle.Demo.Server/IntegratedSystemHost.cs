using Woozle.Host;

using Woozle.Persistence.Ef.Dependencies;

namespace Woozle.Demo.Server
{
    public class IntegratedSystemHost : WoozleHost 
    {
        public IntegratedSystemHost() : base("Woozle Testserver (integrated system)", typeof(WoozleHost).Assembly, typeof(TestService).Assembly)
        {
            
        }

        public override void Configure(Funq.Container container)
        {
            base.Configure(container);
            
            Plugins.Add(new WoozleEntityFrameworkPlugin());
        }
    }
}
