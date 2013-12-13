using ServiceStack.WebHost.Endpoints;

namespace Woozle.Persistence.Ef.Dependencies
{
    /// <summary>
    /// Register all dependencies for using entity framework as or-mapper in Woozle
    /// </summary>
    public class WoozleEntityFrameworkPlugin : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            new WoozleRepositoryDependencies().Register(appHost);
            new WoozlePersistenceDependencies().Register(appHost);
        }
    }
}
