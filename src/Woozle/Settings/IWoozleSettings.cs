using Woozle.Model;

namespace Woozle.Settings
{
    /// <summary>
    /// Holds all global settings configured in your Woozle Application.
    /// </summary>
    public interface IWoozleSettings
    {
        /// <summary>
        /// Returns the configured default mandator for your Woozle Application or null if no default mandator was configured or no mandator was found with the given name.
        /// </summary>
        Mandator DefaultMandator { get; set; }
    }
}
