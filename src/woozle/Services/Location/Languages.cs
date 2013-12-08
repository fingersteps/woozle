using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Location
{
    [Route("/languages", "GET, OPTIONS")]
    public class Languages : IReturn<List<Model.Language>>
    {
    }
}
