namespace Woozle.Host
{
    public class WoozleDefaults
    {
        /// <summary>
        /// The default mandator name used in Woozle when no mandator is necessary (for example in public web services or in user registration)
        /// </summary>
        public string DefaultMandatorName { get; set; }

        /// <summary>
        /// The default language code which is used when no language is set (e.g. "de" or "en)
        /// </summary>
        public string DefaultLanguageCode { get; set; }
    }
}
