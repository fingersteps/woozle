using System.Collections.Generic;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace Woozle.Services.UserManagement
{
	[Route("/users", "GET, OPTIONS")]
    public class UsersDto : IReturn<List<UserSearchResultDto>>
    {
        public UsersDto()
        {
            this.Username = string.Empty;
            this.Firstname = string.Empty;
            this.Lastname = string.Empty;
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class UserSearchResultDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
