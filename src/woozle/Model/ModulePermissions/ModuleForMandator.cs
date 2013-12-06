using Woozle.Model;

namespace Woozle.Core.Model.ModulePermissions
{
    public class ModuleForMandator
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public FixupCollection<Function> Functions { get; set; }

        public Translation Translation { get; set; }
    }
}
