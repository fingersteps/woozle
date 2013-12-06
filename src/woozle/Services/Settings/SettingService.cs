using AutoMapper;
using ServiceStack;
using Woozle.Core.BusinessLogic.Settings;
using Woozle.Core.Services.Stack.ServiceModel;
using Woozle.Core.Services.Stack.ServiceModel.Settings;

namespace Woozle.Core.Services.Stack.Impl.Settings
{
    [Authenticate]
    public class SettingService : AbstractService
    {
        private readonly ISettingsLogic settingsLogic;

        public SettingService(ISettingsLogic settingsLogic)
        {
            this.settingsLogic = settingsLogic;
        }

        /// <summary>
        /// Gets one specific Settings
        /// </summary>
        /// <returns></returns>
        [ExceptionCatcher]
        public Setting Get(Setting requestDto)
        {
            var result = settingsLogic.Load(Session);
            var response = Mapper.Map<Woozle.Model.Setting, Setting>(result);
            return response;
        }

        /// <summary>
        /// Inserts a given object
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<Setting> Post(Setting requestDto)
        {
            return Save(requestDto);
        }

        /// <summary>
        /// Updates a given object
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<Setting> Put(Setting requestDto)
        {
            return Save(requestDto);
        }

        private SaveResult<Setting> Save(Setting requestDto)
        {
            var saveResult = this.settingsLogic.Save(Mapper.Map<Setting, Woozle.Model.Setting>(requestDto), Session);
            var result =
                Mapper.Map<Model.Validation.Creation.ISaveResult<Woozle.Model.Setting>, SaveResult<Setting>>(saveResult);
            return result;
        }
    }
}
