using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Authority
{
    [Route("/mandatorRoles", "GET, OPTIONS")]
    public class MandatorRoles : IReturn<List<MandatorRole>>
    {
    }
}
