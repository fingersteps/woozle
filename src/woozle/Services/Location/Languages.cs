using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.Translation
{
    [Route("/languages", "GET, OPTIONS")]
    public class Languages : IReturn<List<Language>>
    {
    }
}
