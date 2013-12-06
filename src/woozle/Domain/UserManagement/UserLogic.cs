using System.Collections.Generic;
using System.Linq;
using Woozle.Core.BusinessLogic.PermissionManagement;
using Woozle.Core.BusinessLogic.UserManagement;
using Woozle.Core.Common;
using Woozle.Core.Common.PermissionManagement;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Model.UserSearch;
using Woozle.Core.Model.Validation;
using Woozle.Core.Model.Validation.Creation;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.UserManagement
{
    /// <summary>
    /// The implementation for the UserLogic.
    /// </summary>
    public class UserLogic : IUserLogic
    {
        /// <summary>
        /// <see cref="IUserRepository"/>
        /// </summary>
        private new readonly IUserRepository repository;

        /// <summary>
        /// <see cref="IUserValidator"/>
        /// </summary>
        private readonly IUserValidator userValidator;

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
            IUserValidator validator, 
            IUserRepository repository,
            IPermissionManager permissionManager )
        {
            this.repository = repository;
            this.userValidator = validator;
            this.repository = repository;
            this.permissionManager = permissionManager;
        }

        #region IUserLogic Members

        /// <summary>
        /// <see cref="IUserLogic.Search"/>
        /// </summary>
        public IList<UserSearchResult> Search(UserSearchCriteria criteriaUser, Session session)
        {
            if(criteriaUser == null)
            {
                return null;
            }

            var users = this.repository.FindByUserCriteria(criteriaUser, session);

            return users;
        }

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="session">The required session.</param>
        /// <returns><see cref="ISaveResult{TO}"/></returns>
        public new ISaveResult<User> Save(User user, Session session)
        {
            var synchronizedUser = this.repository.Synchronize(user, session);
            userValidator.Session = session;
            userValidator.EditMode = synchronizedUser.Id != 0;

            //Check permissions for creating or editing a new / old user
            if (!(!userValidator.EditMode && (this.permissionManager.HasPermission(session.SessionObject, Constants.LogicalIdDetailUserV1,
                                                  Permissions.PERMISSION_CREATE)) ||
                 (userValidator.EditMode && (this.permissionManager.HasPermission(session.SessionObject, Constants.LogicalIdDetailUserV1,
                                                  Permissions.PERMISSION_EDIT)))))
            {
                throw new NoPermissionException(Constants.LogicalIdSearchUserV1, userValidator.EditMode ? Permissions.PERMISSION_EDIT : Permissions.PERMISSION_CREATE);
            }

            var result = userValidator.Validate(synchronizedUser);
            if (!result.IsValid)
            {
                var creationResult = new SaveResult<User> {TargetObject = user};
                result.Errors.ToList().ForEach(n => creationResult.Errors.Add(new Error(n.PropertyName, n.ErrorMessage)));
                return creationResult;
            }

            this.repository.UnitOfWork.Commit();

            PrepareTargetObject(user, synchronizedUser);
            return new SaveResult<User> { TargetObject = synchronizedUser, HasSystemErrors = false };
        }

        private static void PrepareTargetObject(User userFromClient, User savedUser)
        {
            //Set not mandatory navigation fields to saved object, to avoid re-loading them on client side. TODO: Check to solve this better with EF synchronisation
            var rolesFromClient = userFromClient.UserMandatorRoles;
            var returnedRoles = savedUser.UserMandatorRoles;
            var unchangedOrModifiedRoles =
                rolesFromClient.Where(n => n.PersistanceState == PState.Unchanged || n.PersistanceState == PState.Modified);
            foreach (var role in unchangedOrModifiedRoles)
            {
                if (!returnedRoles.Contains(role)) returnedRoles.Add(role);
            }
            savedUser.UserMandatorRoles = new FixupCollection<UserMandatorRole>(returnedRoles.OrderBy(n => n.Id));
        }

        public void Delete(int id, Session session)
        {
            if (!this.permissionManager.HasPermission(session.SessionObject, Constants.LogicalIdSearchUserV1,
                                                   Permissions.PERMISSION_DELETE))
            {
                throw new NoPermissionException(Constants.LogicalIdSearchUserV1, Permissions.PERMISSION_DELETE);
            }

            var user = this.repository.QueryPrimaryKey(id);
            this.repository.Delete(user, session);
            this.repository.UnitOfWork.Commit();
        }

        public User LoadUser(int id, Session session)
        {
            if (id == 0)
            {
                return null;
            }

            var users = this.repository.LoadUser(id, session);

            return users;
        }

        public IList<User> GetUsersOfMandator(Session session)
        {
            var users = repository.CreateQueryable(session);
            var result = from user in users
                         where
                             user.UserMandatorRoles.FirstOrDefault(n => n.MandatorRole.MandId == session.SessionObject.Mandator.Id) !=
                             null
                         select user;

            var foundUsers = result.ToList();
            return foundUsers;
        }

        #endregion
    }
}
