using System;

namespace Woozle.Common.Exceptions
{
    public class BusinessLogicException : Exception
    {

        public BusinessLogicException(string message)
            : base(message)
        {

        }
    }
}
