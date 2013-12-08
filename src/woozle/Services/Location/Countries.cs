using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Location
{
    [Route("/countries", "GET, OPTIONS")]
    public class Countries : IReturn<List<CountryDto>>
    {
    }
}
