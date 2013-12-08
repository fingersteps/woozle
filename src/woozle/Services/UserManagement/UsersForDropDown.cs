using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.UserManagement
{
    [Route("/usersForDropDown", "GET, OPTIONS")]
    public class UsersForDropDown : IReturn<List<Model.User>>
    {
    }
}
