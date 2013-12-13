using System;
using System.Collections.ObjectModel;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class ModuleGroupDto : WoozleDto
    {
        public ModuleGroupDto()
        {
            this.Modules = new ObservableCollection<ModuleDto>();
        }
    
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public ObservableCollection<ModuleDto> Modules { get; set; }
    }
}
