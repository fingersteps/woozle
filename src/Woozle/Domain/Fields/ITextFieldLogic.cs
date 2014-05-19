using System.Collections.Generic;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Fields
{
    public interface ITextFieldLogic
    {
        /// <summary>
        /// Gets all text fields of the specific mandator.
        /// </summary>
        /// <param name="session"><see cref="SessionData"/></param>
        /// <returns>A list of <see cref="TextFieldSearchResult"/></returns>
        IEnumerable<TextFieldSearchResult> GetTextFields(SessionData session);

        /// <summary>
        /// Gets a specific text field.
        /// </summary>
        /// <param name="name">The name of the text field.</param>
        /// <param name="session"><see cref="SessionData"/></param>
        /// <returns>The <see cref="TextFieldSearchResult"/></returns>
        TextFieldSearchResult GetTextField(string name, SessionData session);
    }
}
