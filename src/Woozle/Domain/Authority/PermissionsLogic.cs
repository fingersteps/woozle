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
        /// <see cref="IPermissionsLogic.GetAssignedPermissions(SessionData)"/>
        /// </summary>
        public IList<FunctionPermission> GetAssignedPermissions(SessionData sessionData)
        {
            log.Debug(string.Format("Getting assigned permissions for user {0}", sessionData.User.Id));
            var users = UserRepository.CreateQueryable(sessionData);
            var query = from user in users
                        from userMandatorRole in user.UserMandatorRoles
                        from functionPermission in userMandatorRole.MandatorRole.FunctionPermissions
                        where user.Id == sessionData.User.Id &&
                              userMandatorRole.MandatorRole.MandId == sessionData.Mandator.Id
                        select new
                        {
                            functionPermission,
                            functionPermission.Permission,
                            functionPermission.Function
                        };

            var functionPermissions = query.ToList().Select(result => result.functionPermission).ToList();
            return functionPermissions;
        }

        public void SaveChangedPermissions(Role role, List<ChangedModulePermission> changedPermissions, SessionData sessionData)
        {
            int mandatorId = sessionData.Mandator.Id;
            var foundMandatorRole =
                this.MandatorRoleRepository.FindByExp(n => n.MandId == mandatorId && n.RoleId == role.Id, sessionData, "FunctionPermissions").
                    FirstOrDefault();
            if (foundMandatorRole != null)
            {
                SaveChangedPermissionsToMandatorRole(foundMandatorRole, changedPermissions, sessionData);
            }
        }

        private void SaveChangedPermissionsToMandatorRole(MandatorRole mandatorRole, IEnumerable<ChangedModulePermission> changedPermissions, SessionData sessionData)
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
            this.MandatorRoleRepository.Save(mandatorRole, sessionData);
            this.MandatorRoleRepository.UnitOfWork.Commit();
        }
    }
}
