using System;
using System.Diagnostics;
using System.Linq;
using PostSharp.Extensibility;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordRequestValidator : IPasswordRequestValidator
    {
        private readonly IRepository<Model.PasswordRequest> passwordRequestRepository;

        public PasswordRequestValidator(IRepository<Model.PasswordRequest> passwordRequestRepository)
        {
            this.passwordRequestRepository = passwordRequestRepository;
        }

        public bool CanRequestPassword(string ip, SessionData sessionData)
        {
            if (string.IsNullOrEmpty(ip))
            {
                const string message = "The ip is null or empty.";
                Trace.TraceError(message);
                throw new ArgumentNullException("ip", message);
            }

            var attemptsQuery = this.passwordRequestRepository.CreateQueryable(sessionData);

            var dateTime = DateTime.Now.AddMinutes(15);

            var attempts = from attempt in attemptsQuery
                            where attempt.IP == ip && attempt.TimeStamp <= dateTime
                            select attempt;

            var numberOfAttempts = attempts.Count();

            return numberOfAttempts < 3;
        }
    }
}
