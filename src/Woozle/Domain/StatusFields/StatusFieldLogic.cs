using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.StatusFields
{
    public class StatusFieldLogic : IStatusFieldLogic
    {
        private readonly IRepository<Status> statusRepository;

        public StatusFieldLogic(IRepository<Status> statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public Status LoadStatusByValue(string statusValue)
        {
            var systemSessionData = new SessionData(null, null);
            return statusRepository.FindByExp(n => n.Value == statusValue, systemSessionData).First();
        }
    }
}
