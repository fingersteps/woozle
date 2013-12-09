using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Location
{
    [Route("/locations", "GET, OPTIONS")]
    public class Locations : WoozleDto, IReturn<List<Model.Location>>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public Nullable<int> CityId { get; set; }

        public Model.City City { get; set; }
        public Mandator.MandatorDto MandatorDto { get; set; }
    }
}
