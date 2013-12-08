using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Location
{
    [Route("/countries", "GET, OPTIONS")]
    public class Countries : IReturn<List<Country>>
    {
    }
}
