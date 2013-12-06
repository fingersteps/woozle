using System.Collections.Generic;
using System.Linq;
using Woozle.Core.BusinessLogic.Authority;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.Authority
{
    /// <summary>
    /// Logic for searching <see cref="Role">roles</see>
    /// </summary>
    public class GetRolesLogic : IGetRolesLogic
    {

        private readonly IRepository<MandatorRole> mandatorRoleRepository;
        
        /// <summary>
        /// ctor.
        /// </summary>
        /// <see cref="IRepository{T}">Repository for getting the role navigation property of the <see cref="Mandator"/>.</see>
        public GetRolesLogic(IRepository<MandatorRole> mandatorRoleRepository)
            {
                this.mandatorRoleRepository = mandatorRoleRepository;
            }

        /// <summary>
        /// <see cref="IGetRolesLogic.GetAllMandatorRolesByMandator"/>
        /// </summary>
        public IList<MandatorRole> GetAllMandatorRolesByMandator(Session session)
        {
            var mandatorRoles = mandatorRoleRepository.CreateQueryable(session);

            var query = from mandatorRole in mandatorRoles
                        where mandatorRole.MandId == session.SessionObject.Mandator.Id ||
                    (session.SessionObject.Mandator.MandatorGroupId != 0 &&
                     mandatorRole.Mandator.MandatorGroupId == session.SessionObject.Mandator.MandatorGroupId)
                        orderby mandatorRole.MandId
                        select new
                        {
                            mandatorRole,
                            mandatorRole.Mandator,
                            mandatorRole.Role
                        };

            return query.ToList().Select(n => n.mandatorRole).ToList();
        }

        public IList<MandatorRole> GetMandatorRolesForMandator(Session session)
        {
            return
                this.mandatorRoleRepository.FindByExp(n => n.MandId == session.SessionObject.Mandator.Id, session,
                                                      "Role", "Mandator").ToList();
        }
    }
}
