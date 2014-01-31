using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            this.Logger = LogManager.GetLogger(this.GetType());
        }

        protected IEfUnitOfWork Context { get; set; }

        public IUnitOfWork UnitOfWork { get; private set; }

        protected ILog Logger { get; set; }

        #region IRepository<T> Members


        public IQueryable<T> CreateQueryable(Session session)
        {
            return Context.Get<T>(session);
        }

        /// <summary>
        /// Counts all entities of the repository
        /// </summary>
        /// <param name="session"></param>
        /// <returns>The number of records</returns>
        public int Count(Session session)
        {
            return Context.Get<T>(session).Count();
        }

        public T Save(T item, Session session)
        {
            item.PersistanceState = item.Id == 0 ? PState.Added : PState.Modified;
            return Context.SynchronizeObject(item, session);
        }

        public abstract T Synchronize(T item, Session session);

        public bool Contains(T item, Session session)
        {
            return Context.Get<T>(session).FirstOrDefault(t => t == item) != null;
        }

        /// <summary>
        /// Performs a delete of the given entity inclusive all its related data acc. to the <see cref="WoozleObject"/> PersistenceState.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="session"></param>
        public abstract void Delete(T item, Session session);

        /// <summary>
        /// Searches for all entities in the repository
        /// </summary>
        /// <param name="session"></param>
        /// <param name="includedEntities">Included entities for the related entities which should be loaded (ATTENTION: Consider creating a separate query because includins entities is slow!)</param>
        /// <returns></returns>
        public virtual List<T> FindAll(Session session, params string[] includedEntities)
        {
            var set = Context.Get<T>(session);
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
        /// <param name="session"></param>
        /// <param name="lazyIncludeStrings"></param>
        /// <returns></returns>
        public virtual List<T> FindByExp(Func<T, bool> predicate, Session session, params string[] lazyIncludeStrings)
        {
            var set = Context.Get<T>(session);
            foreach (string include in lazyIncludeStrings)
            {
                set = set.Include(include);
            }
            return set.Where(predicate).AsQueryable<T>().ToList();
        }

        #endregion

    }
}
