using System.Collections.Generic;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Repository
{
    public interface IStatusFieldRepository : IRepository<StatusField>
    {
        /// <summary>
        /// Gets status information (status records) for a given field.
        /// </summary>
        /// <param name="statusFieldName"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        IList<Status> GetStatusInformation(string statusFieldName, Session session);
    }
}
