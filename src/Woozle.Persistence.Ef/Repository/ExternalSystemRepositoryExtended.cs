using System.Diagnostics;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Persistence.Ef.Repository
{
    public partial class ExternalSystemRepository : IExternalSystemRepository
    {
        #region IExternalServiceRepository Members

        public ExternalSystem FindServiceByMandantAndType(string externalServiceTypeName, SessionData sessionData)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var selection = from service in Context.Get<ExternalSystem>(sessionData)
                            join serviceType in Context.Get<ExternalSystemType>(sessionData) on service.ExternalSystemTypeId
                                equals serviceType.Id
                            where service.MandatorId == sessionData.Mandator.Id &&
                                  serviceType.Name == externalServiceTypeName
                            select service;
            stopwatch.Stop();

            var result = selection.FirstOrDefault();
            this.Logger.Info(string.Format("FindServiceByMandantAndType took {0} ms.", stopwatch.ElapsedMilliseconds));

            return result;
        }

        #endregion
    }
}
