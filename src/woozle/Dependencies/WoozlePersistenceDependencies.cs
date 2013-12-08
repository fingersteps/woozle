using Funq;
using Woozle.Persistence;

namespace Woozle.Dependencies
{
    public class WoozlePersistenceDependencies : IWoozleDependency
    {
        public void Register(Container container)
        {
            container.RegisterAutoWiredAs<EfWoozleEntity, IEfUnitOfWork>();
        }
    }
}
