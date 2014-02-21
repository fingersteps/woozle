using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.PersonManagement
{
    public interface IPersonLogic
    {
        /// <summary>
        /// Checks if the given person is already existing in the database. If so, the existing person will be used for the further steps.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="sessionData"></param>
        /// <returns>A found person or the same person which was given as parameter when there was no matching person found.</returns>
        Person SearchForExistingPerson(Person person, SessionData sessionData);
    }
}
