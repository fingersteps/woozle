using System;

namespace Woozle.Services
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message)
        {
            
        }
    }
}
