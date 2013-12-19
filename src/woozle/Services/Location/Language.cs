using System;

namespace Woozle.Services.Location
{
    [Serializable]
    public partial class Language : WoozleDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    
}
