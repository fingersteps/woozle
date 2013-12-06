using System.Collections.Generic;
using ServiceStack;
using Woozle.Model;

namespace Woozle.Core.Services.Stack.ServiceModel.ModuleManagement
{
    [Route("/modules", "GET, OPTIONS")]
    public class Modules : WoozleDto, IReturn<List<ModuleForMandator>>
    {
    }

    public class ModuleForMandator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public FixupCollection<Function> Functions { get; set; }
        public Translation.Translation Translation { get; set; }
    }
}
