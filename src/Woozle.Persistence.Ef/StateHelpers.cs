using System.Data.Entity;
using Woozle.Model;

namespace Woozle.Persistence.Ef
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
