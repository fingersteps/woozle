using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Fields
{
    public class PlaceHolderLogic : IPlaceholderLogic
    {
        private readonly IRepository<TextFieldPlaceHolder> repository; 

        public PlaceHolderLogic(IRepository<TextFieldPlaceHolder> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<PlaceHolderSearchResult> GetPlaceHolders(SessionData session)
        {
            var query = repository.CreateQueryable(session);
            var languageId = session.User.LanguageId;
            var placeHolders = from textField in query
                               let descriptionTranslation = textField.Translation.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                               let valueTranslation = textField.Translation1.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                               select new PlaceHolderSearchResult
                               {
                                   Id = textField.Id,
                                   Name = textField.Name,
                                   Description = descriptionTranslation.Description,
                                   Value = valueTranslation.Description
                               };


            return placeHolders.ToList();
        }

        public TextFieldPlaceHolder GetPlaceHolder(int id, SessionData session)
        {
            var query = this.repository.CreateQueryable(session);

            var result = from textField in query
                         where textField.Id == id
                         select textField;

            return result.FirstOrDefault();
        }
    }
}
