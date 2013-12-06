using System.Collections.Generic;
using Woozle.Model;

namespace Woozle.Core.Model.UserSearch
{
    public class UserSearchForLoginResult
    {
        public User User { get; set; }
        public IEnumerable<Mandator> Mandators { get; set; }
    }
}
