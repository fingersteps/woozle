using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;

namespace Woozle.Persistence
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
        /// <param name="sessionData"><see cref="Session"/></param>
        /// <returns>A list of <see cref="UserSearchResult"/></returns>
        IList<UserSearchResult> FindByUserCriteria(UserSearchCriteria criteria, SessionData sessionData);

        /// <summary>
        /// Gets the user object and its assigned mandator(s)
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns></returns>
        UserSearchForLoginResult FindForLogin(string username);

        /// <summary>
        /// Loads a user by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        User LoadUser(int id, SessionData sessionData);
    }
}
