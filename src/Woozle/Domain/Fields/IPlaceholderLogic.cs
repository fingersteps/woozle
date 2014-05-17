using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Fields
{
    public interface IPlaceholderLogic
    {
        IEnumerable<PlaceHolderSearchResult> GetPlaceHolders(SessionData session);
        TextFieldPlaceHolder GetPlaceHolder(int id, SessionData session);
    }
}
