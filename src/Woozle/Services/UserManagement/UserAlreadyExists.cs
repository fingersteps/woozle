using ServiceStack.ServiceHost;

namespace Woozle.Services.UserManagement
{
    [Route("/userAlreadyExists", "GET, OPTIONS")]
    public class UserAlreadyExists : IReturn<bool>
    {
        public UserAlreadyExists()
        {
            this.Username = string.Empty;
        }

        public string Username { get; set; }
    }
}
