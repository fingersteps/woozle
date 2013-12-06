using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.PersonManagement
{
    public interface IPersonLogic
    {
        /// <summary>
        /// Checks if the given person is already existing in the database. If so, the existing person will be used for the further steps.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="session"></param>
        /// <returns>A found person or the same person which was given as parameter when there was no matching person found.</returns>
        Person SearchForExistingPerson(Person person, Session session);
    }
}
