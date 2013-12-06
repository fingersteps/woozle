namespace Woozle.Core.BusinessLogic.Authentication
{
    /// <summary>
    /// Exception if the user won't be found.
    /// </summary>
    /// <remarks></remarks>
    public class InvalidLoginException : System.SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLoginException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <remarks></remarks>
        public InvalidLoginException(string message)
            : base(message)
        {
            
        }
    }
}
