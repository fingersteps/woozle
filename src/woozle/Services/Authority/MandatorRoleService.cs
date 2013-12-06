using System.Collections.Generic;
using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.Authority;
using Woozle.Core.Services.Stack.ServiceModel.Authority;

namespace Woozle.Core.Services.Stack.Impl.Authority
{
    [Authenticate]
    public class MandatorRoleService : AbstractService
    {
        private readonly IGetRolesLogic getRolesLogic;

        public MandatorRoleService(IGetRolesLogic getRolesLogic)
        {
            this.getRolesLogic = getRolesLogic;
        }

        /// <summary>
        /// Get all MandatorRoles of the current session
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<MandatorRole> Get(MandatorRoles requestDto)
        {
            var result = getRolesLogic.GetMandatorRolesForMandator(Session);
            return Mapper.Map<IList<Woozle.Model.MandatorRole>, List<MandatorRole>>(result);
        }

        /// <summary>
        /// Get all MandatorRoles of the current session used for drop down menus
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public IList<MandatorRole> Get(MandatorRolesForDropDown requestDto)
        {
            var result = getRolesLogic.GetAllMandatorRolesByMandator(Session);
            return Mapper.Map<IList<Woozle.Model.MandatorRole>, List<MandatorRole>>(result);
        }
    }
}
