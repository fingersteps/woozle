using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;

namespace Woozle.Persistence.Repository
{
    /// <summary>
    /// Interface for a user repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Finds a user by a <see cref="UserSearchCriteria"/>
        /// </summary>
        /// <param name="criteria"><see cref="UserSearchCriteria"/></param>
        /// <param name="session"><see cref="Session"/></param>
        /// <returns>A list of <see cref="UserSearchResult"/></returns>
        IList<UserSearchResult> FindByUserCriteria(UserSearchCriteria criteria, Session session);

        /// <summary>
        /// Gets the user object and its assigned mandator(s)
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="session"><see cref="Session"/></param>
        /// <returns></returns>
        UserSearchForLoginResult FindForLogin(string username, string password, Session session);

        /// <summary>
        /// Loads a user by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        User LoadUser(int id, Session session);
    }
}
