using System;
using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence
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
        /// <param name="sessionData"></param>
        /// <returns></returns>
        IQueryable<T> CreateQueryable(SessionData sessionData);

        /// <summary>
        /// Counts all records of the currents repositoreis WoozleObject type.
        /// </summary>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        int Count(SessionData sessionData);

        /// <summary>
        /// Synchronizes the given object to the current context including all dependencies.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        T Synchronize(T item, SessionData sessionData);

        /// <summary>
        /// Saves the given item to the current context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sessionData"></param>
        T Save(T item, SessionData sessionData);

        /// <summary>
        /// Checks whether the given item is already existing or not.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        bool Contains(T item, SessionData sessionData);

        /// <summary>
        /// Deletes the given item in the current context.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sessionData"></param>
        void Delete(T item, SessionData sessionData);

        /// <summary>
        /// Gets all records of the repositories types and returns them in a list.
        /// </summary>
        /// <param name="sessionData"></param>
        /// <param name="lazyIncludeString"></param>
        /// <returns></returns>
        List<T> FindAll(SessionData sessionData, params string[] lazyIncludeString);

        /// <summary>
        /// Performs a query with the given expression and including arguments.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sessionData"></param>
        /// <param name="lazyIncludeString"></param>
        /// <returns></returns>
        List<T> FindByExp(Func<T, bool> predicate, SessionData sessionData, params string[] lazyIncludeString);

        /// <summary>
        /// Gets a record by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(int id);

        /// <summary>
        /// Gets a record by its id and the needed references.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="neededReferenceProperties"></param>
        /// <param name="neededCollectionProperties"></param>
        /// <returns></returns>
        T FindById(int id, IEnumerable<string> neededReferenceProperties, IEnumerable<string> neededCollectionProperties);
    }
}
