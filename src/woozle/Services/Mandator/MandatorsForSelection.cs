using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Mandator
{
    [Route("/mandatorsForSelection", "GET, OPTIONS")]
    public class MandatorsForSelection : IReturn<List<Mandator>>
    {
    }
}
