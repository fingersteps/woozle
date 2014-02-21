using System.Collections.Generic;
using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.ModuleManagement;

namespace Woozle.Services.Authority
{
    [Authenticate]
    public class RoleService : AbstractService
    {
        private readonly IModuleLogic moduleLogic;

        public RoleService(IModuleLogic moduleLogic)
        {
            this.moduleLogic = moduleLogic;
        }

        /// <summary>
        /// Get all ModulePermissions of the given role
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<ModulePermissionsResult> Get(RoleModulePermissions requestDto)
        {
            var result = moduleLogic.FindModulePermissions(new Woozle.Model.Role() { Id = requestDto.Id }, Session.SessionData);
            var responseDto = Mapper.Map<IList<Woozle.Model.ModulePermissions.ModulePermissionsResult>, List<ModulePermissionsResult>>(result);
            return responseDto;
        }
    }
}
