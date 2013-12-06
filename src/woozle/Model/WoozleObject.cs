using System;
using System.Runtime.Serialization;

namespace Woozle.Core.Model
{
    [DataContract(IsReference = true)]
    [Serializable]
    public abstract class WoozleObject : IIdentifiable
    {
        private bool dirtyFlag;

        public event EventHandler DirtyFlagChanged;

        protected WoozleObject()
        {
            PersistanceState = PState.Unchanged;
        }

        public int Id { get; set; }

        public PState PersistanceState { get; set; }

        public bool Dirty
        {
            get { return dirtyFlag; }
            set
            {
                var oldValue = dirtyFlag;
                dirtyFlag = value;
                if (DirtyFlagChanged != null && oldValue != dirtyFlag)
                {
                    DirtyFlagChanged(this, EventArgs.Empty);
                }
            }
        }

        //TODO: With this property ALL objects have access to Mandator, also all entities which are not mandator capable! Check for separation of WoozleObjects and mandator capable WoozleObjects.
        public Nullable<int> MandatorId { get; set; }

        public abstract void ActivatePropertyChangedEvent(bool resetPersistenceState);
        public abstract void DeactivatePropertyChangedEvent();
    }

    public enum PState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
