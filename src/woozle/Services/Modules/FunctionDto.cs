using System;
using Woozle.Services.Location;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class FunctionDto : WoozleDto
    {
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public string LogicalId { get; set; }
        public int TranslationId { get; set; }
    
    	/// <summary>
        /// To use the translated value directly it needs to be filled explicit
        /// </summary>
        public string TranslatedValue { get; set; }
        public short Sequence { get; set; }
    
        public ModuleDto ModuleDto { get; set; }
        public Translation Translation { get; set; }
    
    }
    
}
