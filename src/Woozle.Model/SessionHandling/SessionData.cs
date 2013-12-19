namespace Woozle.Model.SessionHandling
{
    /// <summary>
    /// Represents a session data object.
    /// </summary>
    /// <remarks></remarks>
    public class SessionData
    {
        /// <summary>
        /// Initializes a new <see cref="SessionData"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mandator"></param>
        public SessionData(User user, Mandator mandator)
        {
            this.User = user;
            this.Mandator = mandator;
        }

        /// <summary>
        /// Gets the session user.
        /// </summary>
        /// <remarks></remarks>
        public User User { get; private set; }

        /// <summary>
        /// Gets the session mandator.
        /// </summary>
        /// <remarks></remarks>
        public Mandator Mandator { get; set; }

        protected bool Equals(SessionData other)
        {
            return Equals(User, other.User) && Equals(Mandator, other.Mandator);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SessionData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((User != null ? User.GetHashCode() : 0)*397) ^ (Mandator != null ? Mandator.GetHashCode() : 0);
            }
        }
    }
}
