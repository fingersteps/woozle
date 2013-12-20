using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Funq;
using Woozle.Domain.ExternalSystem.ExternalSystemFacade;
using Woozle.Persistence;
using Woozle.UnitTest.Domain.ExternalSystem.Testdata;
using Xunit;

namespace Woozle.UnitTest.Domain.ExternalSystem.ExternalSystemFacade
{
    public class ExternalSystemFacadeFactoryTest
    {
        [Fact]
        public void GetExternalSystemFacadeTest()
        {
            var factory = new FunqExternalSystemFacadeFactory(PrepareTestContainer());
            var externalSystemFacade = factory.GetExternalSystemFacade<IExternalTestSystem>();
            Assert.NotNull(externalSystemFacade);
        }

        private Container PrepareTestContainer()
        {
            var container = new Container();
            container.Register<IExternalSystemFacadeFactory>(c => new FunqExternalSystemFacadeFactory(c));
            container.Register<ComposablePartCatalog>(
                c =>
                new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, "Woozle.UnitTest"));

            container.RegisterAutoWiredAs<TestExternalSystemRepository, IExternalSystemRepository>();

            container.Register<IExternalSystemFacade<IExternalTestSystem>>(
                c => new ExternalSystemFacade<IExternalTestSystem>(
                         c.Resolve<IExternalSystemFacadeFactory>(), c.Resolve<IExternalSystemRepository>(),
                         c.Resolve<ComposablePartCatalog>(), "TestSystemV1"));
            container.Register<IExternalSystemFacade<IExternalTestSystem>>(
                c =>
                new ExternalSystemFacade<IExternalTestSystem>(c.Resolve<IExternalSystemFacadeFactory>(),
                                                               c.Resolve<IExternalSystemRepository>(),
                                                               c.Resolve<ComposablePartCatalog>(),
                                                               "TestSystemV1"));
            return container;
        }
    }
}
