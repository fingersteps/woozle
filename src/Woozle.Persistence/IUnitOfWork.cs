using System;

namespace Woozle.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes
        /// </summary>
        void Commit();
    }
}
