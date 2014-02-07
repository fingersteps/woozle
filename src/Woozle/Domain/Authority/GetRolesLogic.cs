using System.Collections.Generic;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Authority
{
    /// <summary>
    /// Logic for searching <see cref="Role">roles</see>
    /// </summary>
    public class GetRolesLogic : IGetRolesLogic
    {

        private readonly IRepository<MandatorRole> mandatorRoleRepository;
        private readonly IRepository<UserMandatorRole> userMandatorRoleRepository;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <see cref="IRepository{T}">Repository for getting the role navigation property of the <see cref="Mandator"/>.</see>
        public GetRolesLogic(IRepository<MandatorRole> mandatorRoleRepository, IRepository<UserMandatorRole> userMandatorRoleRepository)
        {
            this.mandatorRoleRepository = mandatorRoleRepository;
            this.userMandatorRoleRepository = userMandatorRoleRepository;
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


        /// <summary>
        /// <see cref="IGetRolesLogic.GetUserRoles"/>
        /// </summary>
        public List<string> GetUserRoles(SessionData sessionData)
        {
            var userMandatorRoles = userMandatorRoleRepository.CreateQueryable(sessionData);

            var query = from userMandatorRole in userMandatorRoles
                where userMandatorRole.UserId == sessionData.User.Id &&
                      userMandatorRole.MandatorRole.MandId == sessionData.Mandator.Id
                select userMandatorRole.MandatorRole.Role.Name;
            return query.ToList();
        }

        public IList<MandatorRole> GetMandatorRolesForMandator(Session session)
        {
            return
                this.mandatorRoleRepository.FindByExp(n => n.MandId == session.SessionObject.Mandator.Id, session,
                                                      "Role", "Mandator").ToList();
        }
    }
}
