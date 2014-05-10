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
        /// <param name="sessionData">The session</param>
        /// <returns>A list of users</returns>
        IList<UserSearchResult> Search(UserSearchCriteria criteriaUser, SessionData sessionData);

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user"><see cref="User"/></param>
        /// <param name="sessionData"><see cref="Session"/></param>
        /// <returns><see cref="User"/>.</returns>
        User Save(User user, SessionData sessionData);

        /// <summary>
        /// Deletes the specific user.
        /// </summary>
        /// <param name="id"><see cref="User"/></param>
        /// <param name="sessionData"><see cref="Session"/></param>
        void Delete(int id, SessionData sessionData);

        /// <summary>
        /// Loads a user by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionData"></param>
        User LoadUser(int id, SessionData sessionData);

        /// <summary>
        /// Gets all users of the current mandator
        /// </summary>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        IList<User> GetUsersOfMandator(SessionData sessionData);

        /// <summary>
        /// Gets the user of the given id or null if no user was found.
        /// </summary>
        /// <param name="parse"></param>
        /// <returns></returns>
        User FindUserById(int parse);

        /// <summary>
        /// Gets the user by the given username or null if no user was found.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sessionData"></param>
        User GetUserByUsername(string username, SessionData sessionData);

        /// <summary>
        /// Changes the password of the logged in user acc. to the given parameters.
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="sessionData"></param>
        User ChangePassword(string oldPassword, string newPassword, SessionData sessionData);
    }
}
