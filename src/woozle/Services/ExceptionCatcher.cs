using System;
using PostSharp.Aspects;
using ServiceStack.Logging;

namespace Woozle.Services
{
    /// <summary>
    /// Catches all exceptions and parse them into user friendli ServiceExceptions, which gets sent to the Client
    /// </summary>
    [Serializable]
    public class ExceptionCatcher : OnExceptionAspect
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ExceptionCatcher));

        public override void OnException(MethodExecutionArgs args)
        {
            var msg = string.Format("{0} threw the following exception: {1}\n{2}",
                args.Method.Name, args.Exception.Message, args.Exception.StackTrace);
            Logger.Error(msg);
            var message = "The following exception occured: " + args.Exception.Message;
            throw new ServiceException(message);
        }
    }
}
