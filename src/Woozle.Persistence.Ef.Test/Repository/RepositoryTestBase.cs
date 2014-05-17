using System;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef.Test.Repository
{
    /// <summary>
    /// Base Class which supports useful methods to easily test repositories
    /// </summary>
    public abstract class RepositoryTestBase
    {
        /// <summary>
        /// Default Session which can be used in testing environment
        /// </summary>
        protected Session Session { get; private set; }

        /// <summary>
        /// Default Session User which can be used in testing environment
        /// </summary>
        protected User SessionUser { get; private set; }

        /// <summary>
        /// Default Session Mandator which can be used in testing environment
        /// </summary>
        protected Mandator SessionMandator { get; private set; }

        protected RepositoryTestBase()
        {
            this.SessionUser = new User();
            this.SessionMandator = new Mandator();
            var sessionData = new SessionData(SessionUser, SessionMandator);
            this.Session = new Session( sessionData);
        }
    }
}
