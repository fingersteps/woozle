using System;
using Woozle.Core.Model;

namespace Woozle.Core.Services.Stack.ServiceModel
{
    [Serializable]
    public class WoozleDto
    {
        public int Id { get; set; }

        public PState PersistanceState { get; set; }

        public bool Dirty { get; set; }

        public Nullable<int> MandatorId { get; set; }
    }
}
