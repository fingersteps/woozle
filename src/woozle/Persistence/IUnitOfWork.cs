using System;

namespace Woozle.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
