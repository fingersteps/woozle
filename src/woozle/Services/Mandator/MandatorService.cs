using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.MandatorManagement;
using Woozle.Core.Model.Validation.Creation;

namespace Woozle.Core.Services.Stack.Impl.Mandator
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
        public ServiceModel.Mandator.Mandator Get(ServiceModel.Mandator.Mandator mandator)
        {
            var result = mandatorLogic.LoadMandator(Session);
            return Mapper.Map<Woozle.Model.Mandator, ServiceModel.Mandator.Mandator>(result);
        }

        /// <summary>
        /// Updates the given mandator
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public ServiceModel.SaveResult<ServiceModel.Mandator.Mandator> Put(ServiceModel.Mandator.Mandator requestDto)
        {
            var modelObj = Mapper.Map<ServiceModel.Mandator.Mandator, Woozle.Model.Mandator>(requestDto);
            var result = mandatorLogic.Save(modelObj, Session);
            return
                Mapper.Map<ISaveResult<Woozle.Model.Mandator>, ServiceModel.SaveResult<ServiceModel.Mandator.Mandator>>(result);
        }
    }
}
