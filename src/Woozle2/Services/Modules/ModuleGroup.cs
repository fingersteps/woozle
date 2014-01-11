using System;
using System.Collections.ObjectModel;

namespace Woozle.Services.Modules
{
    [Serializable]
    public partial class ModuleGroup : WoozleDto
    {
        public ModuleGroup()
        {
            this.Modules = new ObservableCollection<Module>();
        }
    
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public ObservableCollection<Module> Modules { get; set; }
    }
}
