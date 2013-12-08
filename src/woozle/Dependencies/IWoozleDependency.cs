using Funq;

namespace Woozle.Dependencies
{
    public interface IWoozleDependency
    {
        void Register(Container container);
    }
}
