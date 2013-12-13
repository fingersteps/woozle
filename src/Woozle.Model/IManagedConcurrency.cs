namespace Woozle.Model
{
    /// <summary>
    /// Marker interface to identify entities for which concurrt modifications should be tracked.
    /// </summary>
    public interface IManagedConcurrency
    {
        byte[] ChangeCounter { get; }
    }
}
