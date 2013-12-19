using System;

namespace Woozle.Services.Location
{
    [Serializable]
    public partial class Message : WoozleDto
    {
        public string Code { get; set; }
        public int TranslationId { get; set; }
    
    	/// <summary>
        /// To use the translated value directly it needs to be filled explicit
        /// </summary>
        public string TranslatedValue { get; set; }

        public Translation Translation { get; set; }
    
    }
    
}
