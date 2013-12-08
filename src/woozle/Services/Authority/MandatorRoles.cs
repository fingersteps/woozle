using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Authority
{
    [Route("/mandatorRoles", "GET, OPTIONS")]
    public class MandatorRoles : IReturn<List<MandatorRole>>
    {
    }
}
