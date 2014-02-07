using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        IQueryable<T> Get<T>(Session session) where T : WoozleObject;
        IQueryable<T> Get<T>(SessionData sessionData) where T : WoozleObject;

        T SynchronizeObject<T>(T obj, Session session) where T : WoozleObject;

        TSource LoadRelatedData<TSource>(int id, IEnumerable<string> referenceProperties,
                                         IEnumerable<string> collectionProperties) where TSource : WoozleObject;
        TSource LoadCollection<TSource>(int id, params string[] navigationProperties) where TSource : WoozleObject;
    }
}
