using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Mandator
{
    [Route("/mandatorsForSelection", "GET, OPTIONS")]
    public class MandatorsForSelection : IReturn<List<Mandator>>
    {
    }
}
