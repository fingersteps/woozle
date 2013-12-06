using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    [Route("/mandatorRoles", "GET, OPTIONS")]
    public class MandatorRoles : IReturn<List<MandatorRole>>
    {
    }
}
