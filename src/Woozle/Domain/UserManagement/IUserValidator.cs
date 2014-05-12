namespace Woozle.Domain.UserManagement
{
    public interface IUserValidator
    {
        /// <summary>
        /// Validates a new User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        void ValidateNewUser(string username, string email);

        /// <summary>
        /// Validates the given password of a user
        /// </summary>
        /// <param name="password"></param>
        void ValidateUserPassword(string password);
    }
}
