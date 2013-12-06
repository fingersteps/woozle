using System;

namespace Woozle.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
