using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.UserManagement
{
    [Route("/usersForDropDown", "GET, OPTIONS")]
    public class UsersForDropDown : IReturn<List<User>>
    {
    }
}
