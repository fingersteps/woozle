using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Persistence;

namespace Woozle.Domain.MandatorManagement
{
    public class MandatorLogic : IMandatorLogic
    {
        private readonly IRepository<Mandator> mandatorRepository;

        public MandatorLogic(
            IRepository<Mandator> mandatorRepository)
        {
            this.mandatorRepository = mandatorRepository;
        }

        public Mandator LoadMandator(SessionData sessionData)
        {
            var query = this.mandatorRepository.CreateQueryable(sessionData);

            var result = (from mandator in query

                          where mandator.Id == sessionData.Mandator.Id
                           select new
                                      {
                                          mandator,
                                          mandator.City
                                      }).FirstOrDefault();

            return result != null ? result.mandator : null;
        }

        public Mandator LoadMandator(string mandatorName)
        {
            var systemSessionData = new SessionData(null, null);
            return mandatorRepository.FindByExp(n => n.Name == mandatorName, systemSessionData).FirstOrDefault();
        }

        public ISaveResult<Mandator> Save(Mandator mandator, SessionData sessionData)
        {
            mandator = this.mandatorRepository.Synchronize(mandator, sessionData);
            this.mandatorRepository.UnitOfWork.Commit();
            return new SaveResult<Mandator> { TargetObject = mandator, HasSystemErrors = false };
        }
    }
}
