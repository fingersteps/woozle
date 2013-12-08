using System.Collections.Generic;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace Woozle.Services.UserManagement
{
    [Route("/usersForDropDown", "GET, OPTIONS")]
    public class UsersForDropDown : IReturn<List<Model.User>>
    {
    }
}
