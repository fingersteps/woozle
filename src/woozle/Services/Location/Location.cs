using System;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Location
{
    [Serializable]
    [Route("/locations", "POST,PUT,DELETE")]
    [Route("/locations/{Id}")]
    public class Location : WoozleDto, IReturn<Location>, IReturn<SaveResultDto<Location>>, IReturnVoid 
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public Nullable<int> CityId { get; set; }
    
        public City City { get; set; }
        public Mandator.Mandator Mandator { get; set; }
    }
    
}
