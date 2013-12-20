using System;
using Funq;

namespace Prosa.ExternalSystem.ExternalSystemFacade
{
    public class FunqExternalSystemFacadeFactory : IExternalSystemFacadeFactory
    {
        private readonly Container container;

        public FunqExternalSystemFacadeFactory(Container container)
        {
            this.container = container;
        }

        public IExternalSystemFacade<T> GetExternalSystemFacade<T>() where T : IExternalSystem
        {
            return this.container.Resolve<IExternalSystemFacade<T>>();
        }
    }
}
