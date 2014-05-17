using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.MandatorManagement;
using Woozle.Model.Validation.Creation;
using Woozle.Services.Authority;

namespace Woozle.Services.Mandator
{
    [Authenticate]
    public class MandatorService : AbstractService
    {
        private readonly IMandatorLogic mandatorLogic;

        public MandatorService(IMandatorLogic mandatorLogic)
        {
            this.mandatorLogic = mandatorLogic;
        }

        /// <summary>
        /// Gets the mandator of the currently logged in user
        /// </summary>
        /// <param name="mandator"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public Mandator Get(Mandator mandator)
        {
            var result = mandatorLogic.LoadMandator(Session.SessionData);
            return Mapper.Map<Model.Mandator, Mandator>(result);
        }

        /// <summary>
        /// Updates the given mandator
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        [RequiredRole(Roles.Administrator)]
        public SaveResultDto<Mandator> Put(Mandator request)
        {
            var modelObj = Mapper.Map<Mandator, Woozle.Model.Mandator>(request);
            var result = mandatorLogic.Save(modelObj, Session.SessionData);
            return
                Mapper.Map<ISaveResult<Woozle.Model.Mandator>, SaveResultDto<Mandator>>(result);
        }
    }
}
