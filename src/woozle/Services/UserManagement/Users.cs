using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.UserManagement
{
	[Route("/users", "GET, OPTIONS")]
    public class Users : IReturn<List<UserSearchResult>>
    {
        public Users()
        {
            this.Username = string.Empty;
            this.Firstname = string.Empty;
            this.Lastname = string.Empty;
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class UserSearchResult
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
