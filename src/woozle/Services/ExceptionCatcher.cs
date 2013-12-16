using System;
using System.Data.Entity.Core;
using PostSharp.Aspects;
using ServiceStack.Logging;
using Woozle.Common.Extensions;

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
            throw ParseException(args);
        }

        private ServiceException ParseException(MethodExecutionArgs args)
        {
            var service = GetInstance<AbstractService>(args.Instance);
            var message = "Folgender ungekannter Fehler ist aufgetreten: " + args.Exception.Message;
            var exception = args.Exception;
            var originalException = exception.GetOriginalException();
            if (originalException is OptimisticConcurrencyException)
            {
                message =
                    "Die Daten wurden in der Zwischenzeit von einem anderen Benutzer geändert. \nBitte öffnen Sie das aktuelle Fenster neu und wiederholen Sie Ihre Tätigkeit.";
            }
            else
            {
                message = exception.Message;
            }
            return new ServiceException(message);
        }

        /// <summary>
        /// Gets the instance object in the desired type.
        /// </summary>
        private static T GetInstance<T>(object instanceObj)
        {
            if (instanceObj != null && instanceObj is T)
            {
                return (T)instanceObj;
            }
            var msg = string.Format(string.Format("No instance of type '{0}' was found.", typeof(T)));
            Logger.Error(msg);
            throw new SystemException(msg);
        }
    }
}
