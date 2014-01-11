using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using ServiceStack.Logging;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.ExternalSystem.ExternalSystemFacade
{
    /// <summary>
    /// Facade for getting a specific instance {T} from an external service.
    /// </summary>
    /// <remarks>
    /// The external system facade implements the <see cref="IExternalSystemFacade{T}"/>
    /// </remarks>
    /// <typeparam name="T">Type of the external service which is implementing <see cref="IExternalSystem"/></typeparam>
    public class ExternalSystemFacade<T> : IExternalSystemFacade<T> where T:IExternalSystem
    {
        /// <summary>
        /// <see cref="IExternalSystemRepository"/>
        /// </summary>
        private readonly IExternalSystemRepository externalServiceRepository;

        /// <summary>
        /// <see cref="ILog"/>
        /// </summary>
        private ILog logger = LogManager.GetLogger(typeof(ExternalSystemFacade<T>));

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="externalSystemFacadeFactory">A factory to create external system facades 
        /// (used to let the external systems communicate with each other)</param>
        /// <param name="externalServiceRepository"><see cref="IExternalSystemRepository"/></param>
        /// <param name="catalog"> <see cref="ComposablePartCatalog"/> </param>
        /// <param name="systemType">The external system type name which is used to lookup external systems in the database.</param>
        public ExternalSystemFacade(IExternalSystemFacadeFactory externalSystemFacadeFactory, IExternalSystemRepository externalServiceRepository, ComposablePartCatalog catalog, string systemType)
        {
            if(externalServiceRepository == null)
            {
                const string message = "The external service repository is null.";
                logger.Error(message);
                throw new ArgumentNullException("externalServiceRepository");
            }

            if(catalog == null)
            {
                const string message = "The composable catalog is null. Initializing a default assembly catalog.";
                logger.Warn(message);
                catalog = new AssemblyCatalog(this.GetType().Assembly);
            }

            this.SystemType = systemType;
            this.ExternalSystemFacadeFactory = externalSystemFacadeFactory;
            this.externalServiceRepository = externalServiceRepository;

            //Create the composition container
            var container = new CompositionContainer(catalog);

            // Composable parts are created here i.e. the Import and Export components assembles here
            container.ComposeParts(this);
        }

        /// <summary>
        /// The system tpye for all external systems in the external system facade
        /// </summary>
        private string SystemType { get; set; }

        /// <summary>
        /// Imported external systems which have an <see cref="ExportAttribute"/> 
        /// and are tagged with an <see cref="IExternalSystemMetadata"/> Attribute.
        /// All imported external systems are loaded with the <see cref="Lazy{T}"/> which allows the external
        /// system lazy loaded.
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<T, IExternalSystemMetadata>> ExternalSystems { get; set; }

        /// <summary>
        /// A factory to create ExternalSystemFacade instances. 
        /// This allows each external system to communicate with other desired external systems.
        /// </summary>
        [Export(typeof(IExternalSystemFacadeFactory))]
        private IExternalSystemFacadeFactory ExternalSystemFacadeFactory { get; set; }

        #region IExternalSystemFacade<T> Members

        /// <summary>
        /// <see cref="IExternalSystemFacade{T}.GetExternalSystem"/>
        /// </summary>
        public T GetExternalSystem(Session session)
        {
            var externalService = this.externalServiceRepository.FindServiceByMandantAndType(this.SystemType, session);
            if (externalService != null)
            {
                var externalSystem = FindExternalSystem(externalService);
                if (externalSystem != null)
                {
                    logger.Info(string.Format("Connecting to external system '{0}'.", externalService.Name));
                    return externalSystem;
                }
            }
            return default(T);
        }

        #endregion

        /// <summary>
        /// Gets an instance of the target external service.
        /// </summary>
        /// <remarks>
        /// This method is looking for an instance of the target external system in the ExternalSystems property,
        /// which will be loaded via MEF.
        /// </remarks>
        /// <param name="externalService">
        /// <see cref="Model.ExternalSystem">The external system domain object, which holds all needed information about the target external system</see>
        /// </param>
        /// <returns>A specific instance of the target external system</returns>
        private T FindExternalSystem(Model.ExternalSystem externalService)
        {
            if (ExternalSystems != null)
            {
                var allFoundSystems = from externalSystem in ExternalSystems
                                      let metadata = externalSystem.Metadata
                                      where metadata.Name == externalService.Name
                                      select externalSystem.Value;

                var firstFoundSystem = allFoundSystems.FirstOrDefault();
                if (firstFoundSystem != null)
                {
                    return firstFoundSystem;
                }
            }
            return default(T);
        }
    }
}
