using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Services.Authority
{
    [Route("/mandatorRolesForDropDown", "GET, OPTIONS")]
    public class MandatorRolesForDropDown : IReturn<List<MandatorRole>>
    {
    }
}
