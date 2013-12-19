using System;
using Woozle.Services.Location;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class ModuleDto : WoozleDto
    {
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public int ModuleGroupId { get; set; }
        public string LogicalId { get; set; }
        public Nullable<short> Sequence { get; set; }
        public int TranslationId { get; set; }
    
    	/// <summary>
        /// To use the translated value directly it needs to be filled explicit
        /// </summary>
        public string TranslatedValue { get; set; }
    
        public ModuleGroupDto ModuleGroupDto { get; set; }
        public Translation Translation { get; set; }
    
    }
    
}
