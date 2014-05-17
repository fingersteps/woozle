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
        public Session() : this(new SessionData(new User(), new Mandator()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        /// <param name="sessionData">The session object.</param>
        /// <remarks></remarks>
        public Session(SessionData sessionData)
        {
            this.SessionData = sessionData;
            Roles = new List<string>();
        }

        /// <summary>
        /// Gets the session object.
        /// </summary>
        /// <remarks></remarks>
        public SessionData SessionData { get; set; }

        public override bool HasRole(string role)
        {
            return SessionData.Roles != null && SessionData.Roles.Contains(role);
        }
    }
}
