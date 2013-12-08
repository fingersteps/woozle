using System;
using System.Collections.Generic;
using System.Linq;

using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Repository
{
    public interface IRepository<T> where T : WoozleObject
    {
        /// <summary>
        /// Gets the current used unit of work instance for this repository.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Creates a Queryable object to perform individual queries.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IQueryable<T> CreateQueryable(Session session);

        /// <summary>
        /// Counts all records of the currents repositoreis WoozleObject type.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        int Count(Session session);

        /// <summary>
        /// Synchronizes the given object to the current context including all dependencies.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        T Synchronize(T item, Session session);

        /// <summary>
        /// Saves the given item to the current context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="session"></param>
        T Save(T item, Session session);

        /// <summary>
        /// Checks whether the given item is already existing or not.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        bool Contains(T item, Session session);

        /// <summary>
        /// Deletes the given item in the current context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="session"></param>
        void Delete(T item, Session session);

        /// <summary>
        /// Gets all records of the repositories types and returns them in a list.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="lazyIncludeString"></param>
        /// <returns></returns>
        List<T> FindAll(Session session, params string[] lazyIncludeString);

        /// <summary>
        /// Performs a query with the given expression and including arguments.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="session"></param>
        /// <param name="lazyIncludeString"></param>
        /// <returns></returns>
        List<T>    FindByExp(Func<T, bool> predicate, Session session, params string[] lazyIncludeString);

        /// <summary>
        /// Gets a record by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T QueryPrimaryKey(int id);

        /// <summary>
        /// Gets a record by its id and the needed references.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="neededReferenceProperties"></param>
        /// <param name="neededCollectionProperties"></param>
        /// <returns></returns>
        T QueryPrimaryKey(int id, IEnumerable<string> neededReferenceProperties, IEnumerable<string> neededCollectionProperties);
    }
}
