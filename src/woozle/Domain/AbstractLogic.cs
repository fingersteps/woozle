using ServiceStack.Logging;

namespace Woozle.Domain
{
    public abstract class AbstractLogic
    {
        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILog log;

        protected AbstractLogic()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }
    }
}
