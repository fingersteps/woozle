using System;

namespace Woozle.Core.Common.Aspects
{
    /// <summary>
    /// Exception which is thrown when a desired instance was not found.
    /// </summary>
    public class InstanceNotFoundException : Exception
    {
        public InstanceNotFoundException(string message)
            : base(message)
        {
        }
    }
}
