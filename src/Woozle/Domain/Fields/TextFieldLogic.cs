using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Fields
{
    public class TextFieldLogic : ITextFieldLogic
    {
        private readonly IRepository<TextField> repository; 

        public TextFieldLogic(
            IRepository<TextField> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// <see cref="IReportFieldLogic.GetReportFields"/>
        /// </summary>
        public List<TextFieldSearchResult> GetTextFields(SessionData session)
        {

            var query = this.repository.CreateQueryable(session);
            var languageId = session.User.LanguageId;


            var mandatorFields = from textField in query
                                 let descriptionTranslation = textField.Translation.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                                 let valueTranslation = textField.Translation1.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                                 where textField.MandId == session.Mandator.Id
                                 select new TextFieldSearchResult
                                 {
                                     Id = textField.Id,
                                     Name = textField.Name,
                                     MandId = textField.MandId,
                                     Description = descriptionTranslation.Description,
                                     Value = valueTranslation.Description
                                 };

            var otherFields = from textField in query
                              let descriptionTranslation = textField.Translation.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                              let valueTranslation = textField.Translation1.TranslationItems.FirstOrDefault(n => n.LanguageId == languageId)
                              where mandatorFields.Count(n => n.Name == textField.Name) == 0 && textField.MandId == null
                              select new TextFieldSearchResult
                              {
                                  Id = textField.Id,
                                  Name = textField.Name,
                                  MandId = textField.MandId,
                                  Description = descriptionTranslation.Description,
                                  Value = valueTranslation.Description
                              };

            var result = mandatorFields.Concat(otherFields).ToList();

            return result;
        }

        public TextFieldSearchResult GetTextField(string name, SessionData session)
        {
            return this.GetTextFields(session).FirstOrDefault(n => n.Name == name);
        }
    }
}
