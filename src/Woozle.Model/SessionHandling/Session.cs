using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Funq;
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
        [DataMember]
        public SessionData SessionData { get; set; }

        public override bool HasRole(string role)
        {
            return SessionData.Roles != null && SessionData.Roles.Contains(role);
        }
    }
}
