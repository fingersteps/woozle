using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Location
{
    [Route("/cities", "GET, OPTIONS")]
    public class Cities : IReturn<List<City>>
    {
    }
}
