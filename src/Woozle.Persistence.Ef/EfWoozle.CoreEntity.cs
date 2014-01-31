using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Woozle.Persistence.Ef
{
    public partial class EfWoozleEntity : EfUnitOfWork
    {
        public EfWoozleEntity() : base("name=EfWoozleEntity")
        {
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
