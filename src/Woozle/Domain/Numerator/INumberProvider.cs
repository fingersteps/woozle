using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Domain.Numerator
{
    public interface INumberProvider
    {
        /// <summary>
        /// Gets a number from a <see cref="NumberRange"/> of the number range name
        /// </summary>
        /// <param name="numberRangeName"></param>
        /// <param name="sessionData"><see cref="Session"/></param>
        /// <returns>The new number as a string</returns>
        string GetNextNumber(string numberRangeName, SessionData sessionData);

        /// <summary>
        /// Gets a number from a <see cref="NumberRange"/> of the number range name in a specified format.
        /// </summary>
        /// <param name="numberRangeName"></param>
        /// <param name="sessionData"><see cref="SessionData"/></param>
        /// <param name="format">The specified format.</param>
        /// <returns>The new number as a string</returns>
        string GetNextNumber(string numberRangeName, string format, SessionData sessionData);

    }
}
