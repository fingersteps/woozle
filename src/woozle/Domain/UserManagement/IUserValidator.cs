using ServiceStack.FluentValidation;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.UserManagement
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
