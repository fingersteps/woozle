using System.Collections.Generic;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Authority
{
    /// <summary>
    /// Logic for searchig <see cref="Role">roles</see>
    /// </summary>
    public interface IGetRolesLogic
    {
        /// <summary>
        /// Gets a list of assigned <see cref="MandatorRole"/> of an <see cref="Mandator"/>
        /// </summary>
        /// <param name="session"><see cref="Session"/></param>
        /// <returns></returns>
        IList<MandatorRole> GetAllMandatorRolesByMandator(Session session);

        /// <summary>
        /// Gets all mandatorRoles for the mandator of the given session.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        IList<MandatorRole> GetMandatorRolesForMandator(Session session);
    }
}
