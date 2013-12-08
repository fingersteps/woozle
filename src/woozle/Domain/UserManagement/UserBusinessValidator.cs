using System.Linq;
using ServiceStack.FluentValidation;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence.Repository;

namespace Woozle.Domain.UserManagement
{
    /// <summary>
    /// Validator for the user creation process.
    /// </summary>
    public class UserBusinessValidator : AbstractValidator<User>, IUserValidator
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// ctor.
        /// </summary>
        public UserBusinessValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(user => user.Username).Must(UserNameNotExists).WithLocalizedMessage(() => "The username doesnt exists.");
        }

        #region IUserValidator Members

        /// <summary>
        /// Flag if the use case editing an user or create an user.
        /// </summary>
        public bool EditMode { get; set; }

        /// <summary>
        /// <see cref="Session"/>
        /// </summary>
        public Session Session { get; set; }

        #endregion

        /// <summary>
        /// Checks, if a user with the specified username already exists.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="username">Username</param>
        /// <returns>true, if the username not exists.</returns>
        private bool UserNameNotExists(User user, string username)
        {
            if (!EditMode || user.PersistanceState == PState.Modified)
            {
                var result = this.userRepository.FindByExp(usr => usr.Username == username, this.Session).FirstOrDefault();
                return  result == null || result.Id == user.Id;
            }
            return true;
        }
    }
}
