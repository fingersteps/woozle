using System;

namespace Woozle.Services.Location
{
    [Serializable]
    public partial class Translation : WoozleDto
    {
        public string DefaultDescription { get; set; }
    }
    
}
