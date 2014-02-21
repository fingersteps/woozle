using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.Validation.Creation;

namespace Woozle.Domain.MandatorManagement
{
    public interface IMandatorLogic
    {
        Mandator LoadMandator(SessionData sessionData);
        ISaveResult<Mandator> Save(Mandator mandator, SessionData sessionData);
    }
}
