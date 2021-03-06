﻿using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Authority
{
    /// <summary>
    /// Logic for searchig <see cref="Role">roles</see>
    /// </summary>
    public interface IGetRolesLogic
    {
        /// <summary>
        /// Gets a list of assigned <see cref="MandatorRole"/> of an <see cref="Mandator"/>
        /// </summary>
        /// <param name="sessionData"><see cref="Session"/></param>
        /// <returns></returns>
        IList<MandatorRole> GetAllMandatorRolesByMandator(SessionData sessionData);

        /// <summary>
        /// Gets all mandatorRoles for the mandator of the given session.
        /// </summary>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        IList<MandatorRole> GetMandatorRolesForMandator(SessionData sessionData);

        /// <summary>
        /// Gets a specific mandator role by the given role name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        MandatorRole GetMandatorRoleByName(string roleName, SessionData sessionData);

        /// <summary>
        /// Gets all roles of the given session (user & mandator are considered)
        /// </summary>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        List<string> GetUserRoles(SessionData sessionData);
    }
}
