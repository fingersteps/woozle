using ServiceStack.WebHost.Endpoints;

namespace Woozle.Persistence.Ef.Dependencies
{
    /// <summary>
    /// EF-Context Dependencies
    /// </summary>
    public class WoozlePersistenceDependencies
    {
        public void Register(IAppHost container)
        {
            container.RegisterAs<EfWoozleEntity, IEfUnitOfWork>();
        }
    }
}
