//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Woozle.Core.Model;
using Woozle.Model;

namespace Woozle.Core.Services.Stack.ServiceModel.ModuleManagement
{
    [Serializable]
    public partial class ModuleGroup : WoozleDto
    {
        public ModuleGroup()
        {
            this.Modules = new FixupCollection<Module>();
        }
    
        public byte[] Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public FixupCollection<Module> Modules { get; set; }
    
    }
    
}