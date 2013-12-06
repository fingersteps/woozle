using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.LocationManagement
{
    [Route("/countries", "GET, OPTIONS")]
    public class Countries : IReturn<List<Country>>
    {
    }
}
