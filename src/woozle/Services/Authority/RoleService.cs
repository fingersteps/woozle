using System.Collections.Generic;
using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.ModuleManagement;
using Woozle.Core.Services.Stack.ServiceModel.Authority;

namespace Woozle.Core.Services.Stack.Impl.Authority
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
            var result = moduleLogic.FindModulePermissions(new Woozle.Model.Role() {Id = requestDto.Id}, Session);
            var responseDto = Mapper.Map<IList<Model.ModulePermissions.ModulePermissionsResult>, List<ModulePermissionsResult>>(result);
            return responseDto;
        }
    }
}
