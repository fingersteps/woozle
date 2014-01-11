using System;
using ServiceStack;
using ServiceStack.ServiceHost;

namespace Woozle.Services.Modules.Settings
{
    [Serializable]
    [Route("/settings", "GET,POST,PUT")]
    public partial class SettingDto : WoozleDto, IReturn<SettingDto>, IReturn<SaveResultDto<SettingDto>>
    {
        public string EventManagementPlanningEMail { get; set; }
        public string EventManagementPlanningMobile { get; set; }
        public byte[] ChangeCounter { get; set; }
    
        public Mandator.Mandator Mandator { get; set; }
    
    }
    
}
