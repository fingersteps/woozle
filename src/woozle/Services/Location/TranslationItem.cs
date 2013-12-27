using System;

namespace Woozle.Services.Location
{
    [Serializable]
    public partial class TranslationItem : WoozleDto
    {
        public int TranslationId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
    
        public Language Language { get; set; }
        public Translation Translation { get; set; }
    }
}
