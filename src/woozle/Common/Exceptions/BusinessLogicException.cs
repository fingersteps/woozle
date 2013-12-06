using System;

namespace Woozle.Core.Common.Exceptions
{
    public class BusinessLogicException : Exception
    {

        public BusinessLogicException(string message)
            : base(message)
        {

        }
    }
}
