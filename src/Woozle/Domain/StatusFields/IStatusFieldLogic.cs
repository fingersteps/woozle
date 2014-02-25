using Woozle.Model;

namespace Woozle.Domain.StatusFields
{
    public interface IStatusFieldLogic
    {
        /// <summary>
        /// Loads a Status record by its value.
        /// </summary>
        /// <param name="statusValue"></param>
        /// <returns></returns>
        Status LoadStatusByValue(string statusValue);
    }
}
