using System.Collections.Generic;
using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Authority;
using Woozle.Model;

namespace Woozle.Services.Authority
{
    [Authenticate]
    public class PermissionService : AbstractService
    {
        private readonly IPermissionsLogic permissionLogic;

        public PermissionService(IPermissionsLogic permissionLogic)
        {
            this.permissionLogic = permissionLogic;
        }

        /// <summary>
        /// Get all FunctionPermissions of the current session
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<FunctionPermission> Get(Permissions requestDto)
        {
            var result = permissionLogic.GetAssignedPermissions(Session.SessionData);
            return Mapper.Map<IList<Woozle.Model.FunctionPermission>, List<FunctionPermission>>(result);
        }

        /// <summary>
        /// Updates all given permissions for the given role
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        [RequiredRole(Roles.Administrator)]
        public void Put(SavePermissions requestDto)
        {
            var role = Mapper.Map<Role, Woozle.Model.Role>(requestDto.Role);
            var permissionsToSave = Mapper.Map<List<ChangedModulePermission>,
                List<Woozle.Model.ModulePermissions.ChangedModulePermission>>(requestDto.ChangedPermissions);
            this.permissionLogic.SaveChangedPermissions(role, permissionsToSave, Session.SessionData);
        }
    }
}
