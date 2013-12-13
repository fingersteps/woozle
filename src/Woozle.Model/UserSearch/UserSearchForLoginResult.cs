using System.Collections.Generic;

namespace Woozle.Model.UserSearch
{
    public class UserSearchForLoginResult
    {
        public User User { get; set; }
        public IEnumerable<Mandator> Mandators { get; set; }
    }
}
