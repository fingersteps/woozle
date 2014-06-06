using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using Microsoft.WindowsAzure;

namespace Woozle.Persistence.Ef
{
    public partial class EfWoozleEntity : EfUnitOfWork
    {
        public EfWoozleEntity() : base(CloudConfigurationManager.GetSetting("WoozleDatabaseConnectionString"))
        {
            Trace.TraceInformation("Creating EfWoozleEntity Model.");
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}
