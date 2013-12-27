using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceStack.ServiceHost;
using Woozle.Model;

namespace Woozle.Services.Modules
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
        public ObservableCollection<Function> Functions { get; set; }
        public Translation Translation { get; set; }
    }
}
