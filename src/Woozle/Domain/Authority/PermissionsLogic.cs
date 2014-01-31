using System;
using System.Collections.Generic;
using System.Linq;
using Woozle.Domain.PermissionManagement;
using Woozle.Model;
using Woozle.Model.ModulePermissions;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Authority
{
    public class PermissionsLogic : AbstractLogic, IPermissionsLogic, IPermissionProvider
    {
        private IRepository<MandatorRole> MandatorRoleRepository { get; set; }
        private IRepository<FunctionPermission> FunctionPermissionRepository { get; set; }
        private IRepository<User> UserRepository { get; set; }

        public PermissionsLogic(
            IRepository<MandatorRole> mandatorRoleRepository,
            IRepository<FunctionPermission> functionPermissionRepository,
            IRepository<User> userRepository)
        {
            MandatorRoleRepository = mandatorRoleRepository;
            FunctionPermissionRepository = functionPermissionRepository;
            this.UserRepository = userRepository;
        }

        /// <summary>
        /// <see cref="IPermissionProvider.GetAssignedPermissions(SessionData)"/>
        /// </summary>
        public IList<FunctionPermission> GetAssignedPermissions(SessionData sessionData)
        {
            var session = new Session(Guid.NewGuid(), sessionData, DateTime.Now.AddHours(1));
            return GetAssignedPermissions(session);
        }

        /// <summary>
        /// <see cref="IPermissionsLogic.GetAssignedPermissions(Session)"/>
        /// </summary>
        public IList<FunctionPermission> GetAssignedPermissions(Session session)
        {
            log.Debug(string.Format("Getting assigned permissions for user {0}", session.SessionObject.User.Id));
            var users = UserRepository.CreateQueryable(session);
            var query = from user in users
                        from userMandatorRole in user.UserMandatorRoles
                        from functionPermission in userMandatorRole.MandatorRole.FunctionPermissions
                        where user.Id == session.SessionObject.User.Id &&
                              userMandatorRole.MandatorRole.MandId == session.SessionObject.Mandator.Id
                        select new
                        {
                            functionPermission,
                            functionPermission.Permission,
                            functionPermission.Function
                        };

            var functionPermissions = query.ToList().Select(result => result.functionPermission).ToList();
            return functionPermissions;
        }

        public void SaveChangedPermissions(Role role, List<ChangedModulePermission> changedPermissions, Session session)
        {
            int mandatorId = session.SessionObject.Mandator.Id;
            var foundMandatorRole =
                this.MandatorRoleRepository.FindByExp(n => n.MandId == mandatorId && n.RoleId == role.Id, session, "FunctionPermissions").
                    FirstOrDefault();
            if (foundMandatorRole != null)
            {
                SaveChangedPermissionsToMandatorRole(foundMandatorRole, changedPermissions, session);
            }
        }

        private void SaveChangedPermissionsToMandatorRole(MandatorRole mandatorRole, IEnumerable<ChangedModulePermission> changedPermissions, Session session)
        {
            foreach (var entry in changedPermissions)
            {
                var functionPermission = FunctionPermissionRepository.FindById(entry.FunctionPermissionId);
                if (entry.HasPermission)
                {
                    mandatorRole.FunctionPermissions.Add(functionPermission);
                }
                else
                {
                    var existingPermission = mandatorRole.FunctionPermissions.FirstOrDefault(n => n.Id == functionPermission.Id);
                    mandatorRole.FunctionPermissions.Remove(existingPermission);
                }
            }
            this.MandatorRoleRepository.Save(mandatorRole, session);
            this.MandatorRoleRepository.UnitOfWork.Commit();
        }
    }
}
