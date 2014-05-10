using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
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
        /// <param name="repository"><see cref="IRepository{T}"/></param>
        /// <param name="permissionManager"><see cref="IPermissionManager"/></param>
        public UserLogic(
            IUserRepository repository,
            IPermissionManager permissionManager)
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
            Trace.TraceInformation("Search users.");

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
            Trace.TraceInformation("Save user " + user.Username);

            if (user.Id > 0)
            {
                var selectedUser = this.FindUserById(user.Id);
                selectedUser.Username = user.Username;
                selectedUser.FirstName = user.FirstName;
                selectedUser.FlagActiveStatusId = user.FlagActiveStatusId;
                selectedUser.LanguageId = user.LanguageId;
                selectedUser.LastName = user.LastName;
                selectedUser.Email = user.Email;
                selectedUser.PasswordHash = user.PasswordHash;
                selectedUser.PasswordSalt = user.PasswordSalt;
                selectedUser.PersistanceState = PState.Modified;
                user = selectedUser;
            }
            
            var synchronizedUser = this.repository.Synchronize(user, sessionData);
            this.repository.UnitOfWork.Commit();
            return synchronizedUser;
        }

        public void Delete(int id, SessionData sessionData)
        {
            Trace.TraceInformation("Delete user with the id " + id);

            if (!this.permissionManager.HasPermission(sessionData, Constants.LogicalIdSearchUserV1,
                                                   Permissions.PERMISSION_DELETE))
            {
                throw new NoPermissionException(Constants.LogicalIdSearchUserV1, Permissions.PERMISSION_DELETE);
            }

            var user = this.repository.LoadUser(id, sessionData);
            user.UserMandatorRoles.ForEach(n => n.PersistanceState = PState.Deleted);
            this.repository.Delete(user, sessionData);
            this.repository.UnitOfWork.Commit();
        }

        public User LoadUser(int id, SessionData sessionData)
        {
            Trace.TraceInformation("Loading user with id " + id);

            if (id == 0)
            {
                return null;
            }

            var users = this.repository.LoadUser(id, sessionData);

            return users;
        }

        public IList<User> GetUsersOfMandator(SessionData sessionData)
        {
            Trace.TraceInformation("Get users of a specific mandator.");
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
            Trace.TraceInformation("Find user by a specific id " + id);
            return repository.FindById(id);
        }

        public User GetUserByUsername(string username, SessionData sessionData)
        {
            Trace.TraceInformation("Get user by username " + username);
            return repository.FindByExp(n => n.Username == username, sessionData).FirstOrDefault();
        }
        #endregion
    }
}
