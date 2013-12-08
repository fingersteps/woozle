using System.Collections.Generic;
using AutoMapper;
using ServiceStack;
using Woozle.Domain.ModuleManagement;

namespace Woozle.Services.Modules
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
        public IList<ModuleForMandator> Get(Modules requestDto)
        {
            var result = moduleLogic.GetModulesByMandator(Session);
            return Mapper.Map<IList<Model.ModulePermissions.ModuleForMandator>, List<ModuleForMandator>>(result);
        }
    }
}
