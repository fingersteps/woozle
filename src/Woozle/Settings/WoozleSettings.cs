using Woozle.Model;

namespace Woozle.Settings
{
    public class WoozleSettings : IWoozleSettings
    {
        public Mandator DefaultMandator { get; set; }
        public Language DefaultLanguage { get; set; }
    }
}
