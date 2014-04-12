using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace Woozle.Persistence.Ef
{
    public partial class EfWoozleEntity : EfUnitOfWork
    {
        public EfWoozleEntity() : base("name=EfWoozleEntity")
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
