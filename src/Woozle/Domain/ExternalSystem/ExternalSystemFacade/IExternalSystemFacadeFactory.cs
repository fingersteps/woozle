namespace Woozle.Domain.ExternalSystem.ExternalSystemFacade
{
    /// <summary>
    /// Definition of an factory which creates instances of <see cref="IExternalSystemFacade{T}"/>.
    /// </summary>
    public interface IExternalSystemFacadeFactory
    {
        /// <summary>
        /// Gets an external system facade for the given type.
        /// </summary>
        /// <typeparam name="T">An external system <see cref="IExternalSystem"/></typeparam>
        /// <returns></returns>
        IExternalSystemFacade<T> GetExternalSystemFacade<T>() where T : IExternalSystem;
    }
}
