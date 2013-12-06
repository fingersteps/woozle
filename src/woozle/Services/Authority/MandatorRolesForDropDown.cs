using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Authority
{
    [Route("/mandatorRolesForDropDown", "GET, OPTIONS")]
    public class MandatorRolesForDropDown : IReturn<List<MandatorRole>>
    {
    }
}
