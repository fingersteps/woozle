using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Woozle.Core.Persistence.Impl
{
    public partial class EfWoozleEntity : EfDataContextBase
    {
        //public EfWoozleEntity()
        //    : base()
        //{
        //    this.Configuration.ProxyCreationEnabled = false;
        //    this.Configuration.LazyLoadingEnabled = false;
        //    this.Configuration.AutoDetectChangesEnabled = true;
        //}

        public EfWoozleEntity() : base("name=EfWoozleEntity")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}
