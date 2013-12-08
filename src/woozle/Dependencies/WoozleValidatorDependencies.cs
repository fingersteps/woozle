using Funq;

namespace Woozle.Core.Dependencies
{
    public class WoozleValidatorDependencies : IWoozleDependency
    {
        public void Register(Container container)
        {
          //  container.RegisterAutoWiredAs<UserValidator, IValidator<User>>();
        }
    }
}
