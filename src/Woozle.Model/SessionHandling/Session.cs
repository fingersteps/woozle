using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface.Auth;

namespace Woozle.Model.SessionHandling
{
    /// <summary>
    /// The session
    /// </summary>
    /// <remarks></remarks>
    public class Session : AuthUserSession
    {
        /// <summary>
        /// Initializes a new <see cref="Session"/>
        /// </summary>
        public Session() : this(Guid.NewGuid(), new SessionData(null, null), DateTime.Now.AddHours(1) )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="sessionData">The session object.</param>
        /// <param name="expirationDate">The expiration date.</param>
        /// <remarks></remarks>
        public Session(Guid sessionId, SessionData sessionData, DateTime expirationDate)
        {
            this.Id = sessionId;
            this.SessionData = sessionData;
            this.ExpirationDate = expirationDate;

            Roles = new List<string>();
        }

        /// <summary>
        /// Gets the session id.
        /// </summary>
        /// <remarks></remarks>
        public new Guid Id { get; private set; }

        /// <summary>
        /// Gets the session object.
        /// </summary>
        /// <remarks></remarks>
        public SessionData SessionData { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <remarks></remarks>
        public DateTime ExpirationDate 
        { 
            get;
            set;
        }

        public override bool HasRole(string role)
        {
            return SessionData.Roles != null && SessionData.Roles.Contains(role);
        }
    }
}
