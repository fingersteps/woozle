using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation;
using Woozle.Model.Validation.Creation;
using Woozle.Persistence;

namespace Woozle.Domain.UserManagement
{
    /// <summary>
    /// The implementation for the UserLogic.
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// <see cref="IUserRepository"/>
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// <see cref="IPermissionManager"/>
        /// </summary>
        private readonly IPermissionManager permissionManager;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="validator"><see cref="IUserValidator"/></param>
        /// <param name="repository"><see cref="IRepository{T}"/></param>
        /// <param name="permissionManager"><see cref="IPermissionManager"/></param>
        public UserLogic(
            IUserRepository repository,
            IPermissionManager permissionManager )
        {
            this.repository = repository;
            this.repository = repository;
            this.permissionManager = permissionManager;
        }

        #region IUserLogic Members

        /// <summary>
        /// <see cref="IUserLogic.Search"/>
        /// </summary>
        public IList<UserSearchResult> Search(UserSearchCriteria criteriaUser, SessionData sessionData)
        {
            if(criteriaUser == null)
            {
                return null;
            }

            var users = this.repository.FindByUserCriteria(criteriaUser, sessionData);

            return users;
        }

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="sessionData">The required session.</param>
        /// <returns><see cref="User"/></returns>
        public User Save(User user, SessionData sessionData)
        {
            var synchronizedUser = this.repository.Synchronize(user, sessionData);
            this.repository.UnitOfWork.Commit();
            return synchronizedUser;
        }

        public void Delete(int id, SessionData sessionData)
        {
            if (!this.permissionManager.HasPermission(sessionData, Constants.LogicalIdSearchUserV1,
                                                   Permissions.PERMISSION_DELETE))
            {
                throw new NoPermissionException(Constants.LogicalIdSearchUserV1, Permissions.PERMISSION_DELETE);
            }

            var user = this.repository.FindById(id);
            this.repository.Delete(user, sessionData);
            this.repository.UnitOfWork.Commit();
        }

        public User LoadUser(int id, SessionData sessionData)
        {
            if (id == 0)
            {
                return null;
            }

            var users = this.repository.LoadUser(id, sessionData);

            return users;
        }

        public IList<User> GetUsersOfMandator(SessionData sessionData)
        {
            var users = repository.CreateQueryable(sessionData);
            var result = from user in users
                         where
                             user.UserMandatorRoles.FirstOrDefault(n => n.MandatorRole.MandId == sessionData.Mandator.Id) !=
                             null
                         select user;

            var foundUsers = result.ToList();
            return foundUsers;
        }

        public User FindUserById(int id)
        {
            return repository.FindById(id);
        }

        public User GetUserByUsername(string username, SessionData sessionData)
        {
            return repository.FindByExp(n => n.Username == username, sessionData).FirstOrDefault();
        }

        #endregion
    }
}
