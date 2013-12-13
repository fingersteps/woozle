using Funq;

namespace Woozle.Model
{
    public interface IWoozleDependency
    {
        IWoozleDependency Register(Container container);
    }
}
