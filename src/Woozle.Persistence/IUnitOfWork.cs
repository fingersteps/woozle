using System;

namespace Woozle.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks all made changes
        /// </summary>
        void Rollback();
    }
}
