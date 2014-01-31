using Funq;
using ServiceStack.WebHost.Endpoints;

namespace Woozle.Persistence.Ef.Dependencies
{
    /// <summary>
    /// EF-Context Dependencies
    /// </summary>
    public class WoozlePersistenceDependencies
    {
        public void Register(IAppHost appHost)
        {
            appHost.GetContainer().RegisterAutoWiredAs<EfWoozleEntity, IEfUnitOfWork>().ReusedWithin(ReuseScope.Request);
        }
    }
}
