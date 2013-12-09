using AutoMapper;
using ServiceStack.ServiceInterface;
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
        public MandatorDto Get(MandatorDto mandator)
        {
            var result = mandatorLogic.LoadMandator(Session);
            return Mapper.Map<Model.Mandator, MandatorDto>(result);
        }

        /// <summary>
        /// Updates the given mandator
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResultDto<MandatorDto> Put(MandatorDto requestDto)
        {
            var modelObj = Mapper.Map<MandatorDto, Woozle.Model.Mandator>(requestDto);
            var result = mandatorLogic.Save(modelObj, Session);
            return
                Mapper.Map<ISaveResult<Woozle.Model.Mandator>, SaveResultDto<MandatorDto>>(result);
        }
    }
}
