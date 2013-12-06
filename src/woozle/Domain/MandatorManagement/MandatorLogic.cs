using System.Linq;
using Woozle.Core.BusinessLogic.MandatorManagement;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Model.Validation.Creation;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.MandatorManagement
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
