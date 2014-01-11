using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.UserManagement
{
    [Route("/usersForDropDown", "GET, OPTIONS")]
    public class UsersForDropDownDto : IReturn<List<Model.User>>
    {
    }
}
