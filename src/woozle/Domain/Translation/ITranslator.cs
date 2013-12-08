using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Translation
{
    public interface ITranslator
    {
        /// <summary>
        /// Gets for a text the translation by the language of the user for a given message code.
        /// <remarks>
        ///     If there is no translation it will get back the defaultdescription.
        /// </remarks>
        /// </summary>
        /// <param name="session">The curren <see cref="Session"/></param>
        /// <param name="messgeCode">The cod of the message</param>
        /// <returns>The translated text or null when nothing could be found with the given messageCode.</returns>
        string GetTranslatedText(Session session, string messgeCode);
    }
}
