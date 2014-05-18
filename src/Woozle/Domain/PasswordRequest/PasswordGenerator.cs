using System;
using System.Linq;

namespace Woozle.Domain.PasswordRequest
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string GetRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
        }
    }
}
