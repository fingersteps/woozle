using System.Data;
using System.Data.Entity;
using Woozle.Core.Model;

namespace Woozle.Core.Persistence.Impl
{
    public static class StateHelpers
    {
        public static EntityState GetEquivalentEntityState(PState pState)
        {
            switch (pState)
            {
                case PState.Modified:
                    return EntityState.Added;
                case PState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
