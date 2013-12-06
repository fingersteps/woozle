using System.Collections.Generic;
using ServiceStack;
using Woozle.Core.Services.Stack.ServiceModel.ModuleManagement;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    [Route("/permissions", "GET, OPTIONS")]
    public class Permissions : IReturn<List<FunctionPermission>>
    {

    }
}
