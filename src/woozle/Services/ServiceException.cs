using System;

namespace Woozle.Core.Services.Stack.Impl
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message)
        {
            
        }
    }
}
