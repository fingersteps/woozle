using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using ServiceStack.Logging;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef
{
    public abstract class EfUnitOfWork : DbContext, IEfUnitOfWork
    {
        private const string CHANGE_COUNTER = "ChangeCounter";
        private readonly ILog log = LogManager.GetLogger(typeof(EfUnitOfWork));


        protected EfUnitOfWork(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<EfWoozleEntity>(null);
        }

        #region IUnitOfWork Members

        public virtual IQueryable<T> Get<T>(SessionData sessionData) where T : WoozleObject
        {
            if (typeof(IMandatorCapable).IsAssignableFrom(typeof(T)))
            {
                var result = Set<T>().Where(record => record.MandatorId == sessionData.Mandator.Id);
                return result;
            }
            return Set<T>();
        }

        public T SynchronizeObject<T>(T obj, SessionData sessionData) where T : WoozleObject
        {
            if (obj == null) return null;
            if (typeof (IMandatorCapable).IsAssignableFrom(typeof (T)))
            {
                obj.MandatorId = sessionData.Mandator.Id;
            }
            if(obj.PersistanceState == PState.Added)
            {
                return Set<T>().Add(obj);
            }
            var entry = Entry(Set<T>().Find(obj.Id));
            if (entry != null)
            {
                entry.CurrentValues.SetValues(obj);
                entry.Entity.PersistanceState = obj.PersistanceState;
                UpdateEntityState(entry);
                return entry.Entity;
            }
            return null;
        }

        public TSource LoadCollection<TSource>(int id, params string[] navigationProperties) where TSource : WoozleObject
        {
            return LoadRelatedData<TSource>(id, null, navigationProperties);
        }

        public TSource LoadRelatedData<TSource>(int id, IEnumerable<string> referenceProperties, IEnumerable<string> collectionProperties) where TSource : WoozleObject
        {
            var entry = Entry(Set<TSource>().Find(id));
            if (entry != null)
            {
                if (referenceProperties != null)
                {
                    foreach (string property in referenceProperties)
                    {
                        entry.Reference(property).Load();
                    }
                }

                if (collectionProperties != null)
                {
                    foreach (string property in collectionProperties)
                    {
                        entry.Collection(property).Load();
                    }
                }

                return entry.Entity;
            }
            return null;
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        #endregion

        public override int SaveChanges()
        {
            try
            {
                var changeSet = ChangeTracker.Entries<WoozleObject>().Where(f => f.State != EntityState.Unchanged);
                foreach (var entry in changeSet)
                {
                    SetFinalEntityState(entry);
                    CheckConcurrentModifications(entry);
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    this.log.Error(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                 eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        this.log.Error(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                                     ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw;
            }
        }

        private static void SetFinalEntityState(DbEntityEntry<WoozleObject> entry)
        {
            if (entry.Entity.PersistanceState == PState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else if (entry.Entity.PersistanceState == PState.Unchanged)
            {
                entry.State = EntityState.Unchanged;
            }
            else
            {
                entry.State = entry.Entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            }
        }

        private static void CheckConcurrentModifications(DbEntityEntry<WoozleObject> entry)
        {
            if (entry.Entity is IManagedConcurrency && entry.State == EntityState.Modified)
            {
                // if any timestamps have changed, throw concurrency exception
                if (!entry.CurrentValues.GetValue<byte[]>(CHANGE_COUNTER).SequenceEqual(
                    entry.OriginalValues.GetValue<byte[]>(CHANGE_COUNTER)))
                {
                    throw new OptimisticConcurrencyException();
                }
            }
        }

        private void UpdateEntityState<T>(DbEntityEntry<T> entry) where T : WoozleObject
        {
            switch (entry.Entity.PersistanceState)
            {
                case PState.Added:
                    entry.State = EntityState.Added;
                    break;
                case PState.Modified:
                    entry.State = EntityState.Modified;
                    break;
                case PState.Deleted:
                    entry.State = EntityState.Deleted;
                    break;
            }
        }
    }
}
