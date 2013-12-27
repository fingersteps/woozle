using System.Collections.Generic;
using AutoMapper;
using Woozle.Domain.ModuleManagement;
using Woozle.Services.Authentication;

namespace Woozle.Services.Navigation
{
    [MandatorAuthenticate]
    public class NavigationService : AbstractService
    {
        private readonly IModuleLogic moduleLogic;

        public NavigationService(IModuleLogic moduleLogic)
        {
            this.moduleLogic = moduleLogic;
        }

        /// <summary>
        /// Gets all modules of the mandator of the currently logged in user
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<Header> Get(Navigation requestDto)
        {
            var modules = moduleLogic.GetModulesByMandator(Session);
            var result = Mapper.Map<IList<Woozle.Model.ModulePermissions.ModuleForMandator>, List<Header>>(modules);
            return result;
        }
    }
}
