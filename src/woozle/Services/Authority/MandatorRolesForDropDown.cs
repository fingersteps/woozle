using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Authority
{
    [Route("/mandatorRolesForDropDown", "GET, OPTIONS")]
    public class MandatorRolesForDropDown : IReturn<List<MandatorRole>>
    {
    }
}
