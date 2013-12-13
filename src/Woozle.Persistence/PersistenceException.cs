using System;

namespace Woozle.Persistence
{
    public class PersistenceException : Exception
    {
        public PersistenceException(PersistenceOperation operation, Exception e) : base(e.Message, e)
        {
            this.Operation = operation;
        }

        public PersistenceOperation Operation { get; private set; }
    }
}
