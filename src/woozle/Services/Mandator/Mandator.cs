using System;
using ServiceStack.ServiceHost;
using Woozle.Services.Location;

namespace Woozle.Services.Mandator
{
    [Serializable]
    [Route("/mandator", "GET, PUT")]
    public partial class Mandator : WoozleDto, IReturn<Mandator>, IReturn<SaveResultDto<Mandator>>
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public Nullable<int> CityId { get; set; }
        public byte[] Picture { get; set; }
        public byte[] ChangeCounter { get; set; }
        public string Email { get; set; }
        public Nullable<int> MandatorGroupId { get; set; }
    
        public City City { get; set; }
        public MandatorGroup MandatorGroup { get; set; }
    
    }
    
}
