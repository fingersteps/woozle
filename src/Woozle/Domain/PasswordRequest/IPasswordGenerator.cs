namespace Woozle.Domain.PasswordRequest
{
    public interface IPasswordGenerator
    {
        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>A random password.</returns>
        string GetRandomPassword();
    }
}
