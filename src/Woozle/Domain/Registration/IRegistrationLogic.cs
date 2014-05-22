using Woozle.Model;

namespace Woozle.Domain.Registration
{
    public interface IRegistrationLogic
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        User RegisterUser(User user, string password);
    }
}
