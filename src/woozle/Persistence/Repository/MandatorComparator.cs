using System.Collections.Generic;
using Woozle.Model;

namespace Woozle.Persistence.Repository
{
    public class MandatorComparator : IEqualityComparer<Mandator>
    {
        public bool Equals(Mandator x, Mandator y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Mandator obj)
        {
            return obj.GetHashCode();
        }
    }
}
