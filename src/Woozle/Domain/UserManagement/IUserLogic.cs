using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;

namespace Woozle.Domain.UserManagement
{
    /// <summary>
    /// Definition of a UserLogic.
    /// </summary>
    public interface IUserLogic
    {
        /// <summary>
        /// Searchs users by given criteria.
        /// </summary>
        /// <param name="criteriaUser">The user criteria</param>
        /// <param name="session">The session</param>
        /// <returns>A list of users</returns>
        IList<UserSearchResult> Search(UserSearchCriteria criteriaUser, Session session);

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        /// <param name="session"><see cref="Session"/></param>
        /// <returns><see cref="ISaveResult{TO}"/>.</returns>
        ISaveResult<User> Save(User user, Session session);

        /// <summary>
        /// Deletes the specific user.
        /// </summary>
        /// <param name="id"><see cref="User"/></param>
        /// <param name="session"><see cref="Session"/></param>
        void Delete(int id, Session session);

        /// <summary>
        /// Loads a user by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="session"></param>
        User LoadUser(int id, Session session);

        /// <summary>
        /// Gets all users of the current mandator
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IList<User> GetUsersOfMandator(Session session);
    }
}
