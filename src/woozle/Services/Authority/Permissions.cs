using System.Collections.Generic;
using ServiceStack.ServiceHost;
using Woozle.Services.Modules;

namespace Woozle.Services.Authority
{
    [Route("/permissions", "GET, OPTIONS")]
    public class Permissions : IReturn<List<FunctionPermission>>
    {

    }
}
