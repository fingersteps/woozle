using AutoMapper;
using ServiceStack;
using Woozle.Domain.MandatorManagement;
using Woozle.Model.Validation.Creation;

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
            var result = mandatorLogic.LoadMandator(Session);
            return Mapper.Map<Model.Mandator, Mandator>(result);
        }

        /// <summary>
        /// Updates the given mandator
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<Mandator> Put(Mandator requestDto)
        {
            var modelObj = Mapper.Map<Mandator, Woozle.Model.Mandator>(requestDto);
            var result = mandatorLogic.Save(modelObj, Session);
            return
                Mapper.Map<ISaveResult<Woozle.Model.Mandator>, SaveResult<Mandator>>(result);
        }
    }
}
