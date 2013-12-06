using System.Collections.Generic;
using Woozle.Core.Model;
using Woozle.Model;

namespace Woozle.Core.Persistence.Repository.Impl.Comparator
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
