using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Model.Validation.Creation;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.MandatorManagement
{
    public interface IMandatorLogic
    {
        Mandator LoadMandator(Session session);
        ISaveResult<Mandator> Save(Mandator mandator, Session session);
    }
}
