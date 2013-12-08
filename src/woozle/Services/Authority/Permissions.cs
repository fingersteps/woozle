using System.Collections.Generic;
using ServiceStack;
using Woozle.Services.Modules;

namespace Woozle.Services.Authority
{
    [Route("/permissions", "GET, OPTIONS")]
    public class Permissions : IReturn<List<FunctionPermission>>
    {

    }
}
