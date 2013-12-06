using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.LocationManagement
{
    [Route("/cities", "GET, OPTIONS")]
    public class Cities : IReturn<List<City>>
    {
    }
}
