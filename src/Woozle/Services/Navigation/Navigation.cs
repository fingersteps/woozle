using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Navigation
{
    [Route("/navigation", "GET, OPTIONS")]
    public class Navigation : IReturn<List<Header>>
    {
    }

    public class Header
    {
        public string TranslatedValue { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string LogicalId { get; set; }
        public string TranslatedValue { get; set; }
    }
}
