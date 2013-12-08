using Funq;
using Woozle.Core.Persistence.Impl;

namespace Woozle.Core.Dependencies
{
    public class WoozlePersistenceDependencies : IWoozleDependency
    {
        public void Register(Container container)
        {
            container.RegisterAutoWiredAs<EfWoozleEntity, IEfUnitOfWork>();
        }
    }
}
