using System.Collections.Generic;
using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.ModuleManagement;
using Woozle.Core.Services.Stack.ServiceModel.ModuleManagement;

namespace Woozle.Core.Services.Stack.Impl.Modules
{
    [Authenticate]
    public class ModuleService : AbstractService
    {
        private readonly IModuleLogic moduleLogic;

        public ModuleService(IModuleLogic moduleLogic)
        {
            this.moduleLogic = moduleLogic;
        }

        /// <summary>
        /// Gets all modules of the mandator of the currently logged in user
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<ModuleForMandator> Get(ServiceModel.ModuleManagement.Modules requestDto)
        {
            var result = moduleLogic.GetModulesByMandator(Session);
            return Mapper.Map<IList<Model.ModulePermissions.ModuleForMandator>, List<ModuleForMandator>>(result);
        }
    }
}
