using System;
using System.Diagnostics;
using System.Linq;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.Numerator
{
    public class NumberProvider : INumberProvider
    {
        private readonly IRepository<NumberRange> numberRangeRepository;

        private static volatile object lockObj = new object();

        public NumberProvider(IRepository<NumberRange> numberRangeRepository)
        {
            this.numberRangeRepository = numberRangeRepository;
        }

        public string GetNextNumber(string numberRangeName, SessionData sessionData)
        {
            return GetNextNumber(numberRangeName, null, sessionData);
        }

        public string GetNextNumber(string numberRangeName, string format, SessionData sessionData)
        {
            Trace.TraceInformation("Waiting for getting a number...");

            lock (lockObj)
            {
                var numberRange = (from numberRage in numberRangeRepository.CreateQueryable(sessionData)
                    where numberRage.Name == numberRangeName
                    select numberRage).FirstOrDefault();

                if (numberRange != null)
                {
                    Trace.TraceInformation("Numberrange found. Oldvalue: {0}", numberRange.Current);

                    if (numberRange.Current == null)
                    {
                        numberRange.Current = numberRange.From;
                        return SaveAndFormatNumberString(format, sessionData, numberRange);
                    }

                    if (CheckFromTill(numberRange))
                    {
                        numberRange.Current += 1;
                        return SaveAndFormatNumberString(format, sessionData, numberRange);
                    }
                    Trace.TraceWarning("Numberrange is empty! Current: {0}, Till: {1}", numberRange.Current, numberRange.Till);
                }

                Trace.TraceWarning("Warn couldn't find any numberrange record for the specified mandator.");
            }

            return string.Empty;
        }

        private static bool CheckFromTill(NumberRange numberRange)
        {
            return !numberRange.Till.HasValue
                   || (numberRange.Till.HasValue && numberRange.Current < numberRange.Till);
        }

        private string SaveAndFormatNumberString(string format, SessionData sessionData, NumberRange numberRange)
        {
            var number = SaveCurrentNumber(sessionData, numberRange);
            return FormatNumberString(format, number);
        }

        private static string FormatNumberString(string format, int? number)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return String.Format(format, number);
            }
            return number.ToString();
        }

        private int? SaveCurrentNumber(SessionData sessionData, NumberRange numberRange)
        {
            Trace.TraceInformation("New value: {0}", numberRange.Current);
            this.numberRangeRepository.Synchronize(numberRange, sessionData);
            this.numberRangeRepository.Save(numberRange, sessionData);
            var number = numberRange.Current;
            return number;
        }
    }
}
