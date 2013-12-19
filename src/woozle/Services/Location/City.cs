using System;

namespace Woozle.Services.Location
{
    [Serializable]
    public partial class City : WoozleDto
    {
        public string ZipCode { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
    
}
