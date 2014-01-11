using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Location
{
    [Route("/languages", "GET, OPTIONS")]
    public class Languages : IReturn<List<Model.Language>>
    {
    }
}
