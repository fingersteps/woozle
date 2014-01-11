using System;

namespace Woozle.Services.Mandator
{
    [Serializable]
    public partial class MandatorGroup : WoozleDto
    {
        public string Name { get; set; }
    }
}
