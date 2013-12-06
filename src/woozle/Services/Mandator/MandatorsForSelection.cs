using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Mandator
{
    [Route("/mandatorsForSelection", "GET, OPTIONS")]
    public class MandatorsForSelection : IReturn<List<Mandator>>
    {
    }
}
