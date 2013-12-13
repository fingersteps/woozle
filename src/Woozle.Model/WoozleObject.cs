using System;

namespace Woozle.Model
{
    public abstract class WoozleObject : IIdentifiable
    {
        protected WoozleObject()
        {
            PersistanceState = PState.Unchanged;
        }

        public int Id { get; set; }

        public PState PersistanceState { get; set; }

        //TODO: With this property ALL objects have access to Mandator, also all entities which are not mandator capable! Check for separation of WoozleObjects and mandator capable WoozleObjects.
        public Nullable<int> MandatorId { get; set; }
    }

    public enum PState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
