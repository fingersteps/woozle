using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Mandator
{
    [Route("/mandatorsForSelection", "GET, OPTIONS")]
    public class MandatorsForSelectionDto : IReturn<List<MandatorDto>>
    {
    }
}
