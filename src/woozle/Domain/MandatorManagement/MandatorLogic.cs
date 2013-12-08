using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;
using Woozle.Persistence.Repository;

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

        public Mandator LoadMandator(Session session)
        {
            var query = this.mandatorRepository.CreateQueryable(session);

            var result = (from mandator in query
                       
                           where mandator.Id == session.SessionObject.Mandator.Id
                           select new
                                      {
                                          mandator,
                                          mandator.City
                                      }).FirstOrDefault();

            return result != null ? result.mandator : null;
        }

        public ISaveResult<Mandator> Save(Mandator mandator, Session session)
        {
            mandator = this.mandatorRepository.Synchronize(mandator, session);
            this.mandatorRepository.UnitOfWork.Commit();
            return new SaveResult<Mandator> { TargetObject = mandator, HasSystemErrors = false };
        }
    }
}
