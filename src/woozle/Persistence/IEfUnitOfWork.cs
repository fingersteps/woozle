using System.Collections.Generic;
using System.Linq;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;

namespace Woozle.Core.Persistence.Impl
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        IQueryable<T> Get<T>(Session session) where T : WoozleObject;
        T SynchronizeObject<T>(T obj, Session session) where T : WoozleObject;

        TSource LoadRelatedData<TSource>(int id, IEnumerable<string> referenceProperties,
                                         IEnumerable<string> collectionProperties) where TSource : WoozleObject;
        TSource LoadCollection<TSource>(int id, params string[] navigationProperties) where TSource : WoozleObject;
    }
}
