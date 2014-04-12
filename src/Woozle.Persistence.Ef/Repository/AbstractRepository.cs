using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using ServiceStack.Logging;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef.Repository
{
    /// <summary>
    /// Abstract implementation of a repository which holds some general useful functionalities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractRepository<T> : IRepository<T> where T : WoozleObject
    {
        protected AbstractRepository(IEfUnitOfWork unitOfWork)
        {
            this.Context = unitOfWork;
            this.UnitOfWork = unitOfWork;
        }

        protected IEfUnitOfWork Context { get; set; }

        public IUnitOfWork UnitOfWork { get; private set; }

        #region IRepository<T> Members


        public IQueryable<T> CreateQueryable(SessionData sessionData)
        {
            Trace.TraceInformation("Create queryable of type " + typeof(T));
            return Context.Get<T>(sessionData);
        }

        /// <summary>
        /// Counts all entities of the repository
        /// </summary>
        /// <param name="sessionData"></param>
        /// <returns>The number of records</returns>
        public int Count(SessionData sessionData)
        {
            Trace.TraceInformation("Counting records.");
            return Context.Get<T>(sessionData).Count();
        }

        public T Save(T item, SessionData sessionData)
        {
            Trace.TraceInformation("Saving record of type " + typeof(T));
            item.PersistanceState = item.Id == 0 ? PState.Added : PState.Modified;
            return Context.SynchronizeObject(item, sessionData);
        }

        public abstract T Synchronize(T item, SessionData sessionDatan);

        public bool Contains(T item, SessionData sessionData)
        {
            Trace.TraceInformation("Check if there is already a record of type " + typeof(T));
            return Context.Get<T>(sessionData).FirstOrDefault(t => t == item) != null;
        }

        /// <summary>
        /// Deletes the given entity and synchronizes it with the entity framework context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sessionData"></param>
        public void Delete(T item, SessionData sessionData)
        {
            Trace.TraceInformation("Deleting a record of type " + typeof(T));
            item.PersistanceState = PState.Deleted;
            Synchronize(item, sessionData);
        }

        /// <summary>
        /// Searches for all entities in the repository
        /// </summary>
        /// <param name="sessionData"></param>
        /// <param name="includedEntities">Included entities for the related entities which should be loaded (ATTENTION: Consider creating a separate query because includins entities is slow!)</param>
        /// <returns></returns>
        public virtual List<T> FindAll(SessionData sessionData, params string[] includedEntities)
        {
            Trace.TraceInformation("Find all records of type " + typeof(T));
            var set = Context.Get<T>(sessionData);
            foreach (string include in includedEntities)
            {
                set = set.Include(include);
            }
            return set.ToList();
        }

        /// <summary>
        /// Queries a record acc. to its primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindById(int id)
        {
            return Context.LoadRelatedData<T>(id, null, null);
        }

        /// <summary>
        /// Queries a record acc. to its primary key
        /// </summary>
        /// <param name="id"></param>
        /// <param name="neededReferenceProperties">Entities which should be load for the record of the given primary key (1:n).</param>
        /// <param name="neededCollectionProperties">Entities which should be load for the record of the given primary key (n:m).</param>
        /// <returns></returns>
        public virtual T FindById(int id, IEnumerable<string> neededReferenceProperties,
                                         IEnumerable<string> neededCollectionProperties)
        {
            return Context.LoadRelatedData<T>(id, neededReferenceProperties, neededCollectionProperties);
        }

        /// <summary>
        /// Searches for records which match the given expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sessionData"></param>
        /// <param name="lazyIncludeStrings"></param>
        /// <returns></returns>
        public virtual List<T> FindByExp(Func<T, bool> predicate, SessionData sessionData, params string[] lazyIncludeStrings)
        {
            var set = Context.Get<T>(sessionData);
            foreach (string include in lazyIncludeStrings)
            {
                set = set.Include(include);
            }
            return set.Where(predicate).AsQueryable<T>().ToList();
        }

        #endregion

    }
}
