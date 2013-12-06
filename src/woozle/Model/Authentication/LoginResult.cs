using System.Collections.Generic;
using Woozle.Core.Model.SessionHandling;
using Woozle.Model;

namespace Woozle.Core.Model.Authentication
{
    public class LoginResult
    {
        public LoginResult(SessionData sessionData,
            bool loginSuccessful)
            : this(sessionData, loginSuccessful, false, null)
        {

        }

        public LoginResult(SessionData sessionData,
            bool loginSuccessful, bool checkMandators,
            IEnumerable<Mandator> suggestedMandators)
        {
            this.SessionData = sessionData;
            this.LoginSuccessful = loginSuccessful;
            this.CheckMandators = checkMandators;
            this.SuggestedMandators = suggestedMandators;
        }

        #region ILoginResult<SessionData> Members

        /// <summary>
        /// SessionData
        /// </summary>
        public SessionData SessionData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether login was successfully.
        /// </summary>
        /// <remarks></remarks>
        public virtual bool LoginSuccessful { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the user should choose a mandator.
        /// The suggested mandators will be found <see cref="SuggestedMandators">here</see>.
        /// </summary>
        /// <remarks></remarks>
        public virtual bool CheckMandators { get; private set; }

        /// <summary>
        /// Gets the suggested mandators.
        /// </summary>
        /// <remarks></remarks>
        public virtual IEnumerable<Mandator> SuggestedMandators { get; private set; }

        #endregion
    }
}
