using System;

namespace Woozle.Core.Common.Aspects
{
    /// <summary>
    /// Exception which is thrown when a desired argument of a method was not found.
    /// </summary>
    public class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException(string message)
            : base(message)
        {

        }
    }
}
