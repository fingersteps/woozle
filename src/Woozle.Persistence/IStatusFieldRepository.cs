using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence
{
    public interface IStatusFieldRepository : IRepository<StatusField>
    {
        /// <summary>
        /// Gets status information (status records) for a given field.
        /// </summary>
        /// <param name="statusFieldName"></param>
        /// <param name="sessionData"></param>
        /// <returns></returns>
        IList<Status> GetStatusInformation(string statusFieldName, SessionData sessionData);
    }
}
