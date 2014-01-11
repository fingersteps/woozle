using System;
using Woozle.Model;

namespace Woozle.Services
{
    [Serializable]
    public class WoozleDto
    {
        public int Id { get; set; }

        public PState PersistanceState { get; set; }

        public Nullable<int> MandatorId { get; set; }
    }
}
