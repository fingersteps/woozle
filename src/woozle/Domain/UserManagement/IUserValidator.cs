
using ServiceStack.FluentValidation;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.UserManagement
{
    /// <summary>
    /// Marker interface for a user validator.
    /// </summary>
    public interface IUserValidator : IValidator<User>
    {
        /// <summary>
        /// Check if the user will be edited.
        /// </summary>
        bool EditMode { get; set; }

        /// <summary>
        /// <see cref="Session"/>
        /// </summary>
        Session Session { get; set; }
    }
}
