using System.Collections.ObjectModel;

namespace Woozle.Model.ModulePermissions
{
    public class ModuleForMandator
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public ObservableCollection<Function> Functions { get; set; }

        public Translation Translation { get; set; }
    }
}
