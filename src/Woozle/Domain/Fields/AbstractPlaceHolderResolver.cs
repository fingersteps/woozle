using System;
using System.ComponentModel;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Fields
{
    public abstract class AbstractPlaceHolderResolver<T>
    {
        private readonly IPlaceholderLogic placeHolderLogic;

        protected AbstractPlaceHolderResolver(IPlaceholderLogic placeHolderLogic)
        {
            this.placeHolderLogic = placeHolderLogic;
        }

        /// <summary>
        /// Resolves all found placeHolders in the given text. Calls <see cref="GetPlaceHolderText"/> to resolve the specific placeholders.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        protected string ResolveAllPlaceHolders(string text, SessionData session)
        { 
            var placeHolders = this.placeHolderLogic.GetPlaceHolders(session);

            var replaceText = text;

            foreach (var placeHolder in placeHolders)
            {
                if (text.Contains(placeHolder.Value))
                {
                    var placeHolderEnum = GetValueFromDescription<T>(placeHolder.Name);

                    var placeHolderText = GetPlaceHolderText(placeHolderEnum, session);
                    replaceText = replaceText.Replace(placeHolder.Value, placeHolderText);
                }
            }

            return replaceText;
        }

        protected abstract string GetPlaceHolderText(T placeHolderEnum, SessionData session);

        private T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
    }
}
