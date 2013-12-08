using Funq;

namespace Woozle.Core.Dependencies
{
    public interface IWoozleDependency
    {
        void Register(Container container);
    }
}
