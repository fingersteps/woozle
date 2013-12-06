using System;
using System.Collections.Generic;
using ServiceStack;

namespace Woozle.Core.Services.Stack.ServiceModel.LocationManagement
{
    [Route("/locations", "GET, OPTIONS")]
    public class Locations : WoozleDto, IReturn<List<Location>>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public Nullable<int> CityId { get; set; }

        public City City { get; set; }
        public Mandator.Mandator Mandator { get; set; }
    }
}
