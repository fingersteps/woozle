using System;
using System.Text.RegularExpressions;
using ServiceStack.Common.Extensions;

namespace Woozle.Domain.UserManagement
{
    public class UserValidator : IUserValidator
    {
        //http://stackoverflow.com/questions/3588623/c-sharp-regex-for-a-username-with-a-few-restrictions
        public Regex ValidUserNameRegEx = new Regex(@"^(?=.{3,15}$)([A-Za-z0-9][._-]?)*$", RegexOptions.Compiled);

        public Regex ValidPasswordRegex = new Regex(@"^(.{5,20}$)", RegexOptions.Compiled);

        public void ValidateNewUser(string username, string email)
        {
            username.ThrowIfNull("updatedUser");

            if (username.IsNullOrEmpty())
                throw new ArgumentNullException("UserName is required");

            if (email.IsNullOrEmpty())
                throw new ArgumentNullException("EMail is required");

            if (!username.IsNullOrEmpty())
            {
                if (!ValidUserNameRegEx.IsMatch(username))
                {
                    throw new ArgumentException("UserName contains invalid characters", "UserName");
                }
            }
        }

        public void ValidateUserPassword(string password)
        {
            password.ThrowIfNullOrEmpty("password");
            if (!ValidPasswordRegex.IsMatch(password))
            {
                throw new ArgumentException("Password contains invalid characters", "Password");
            }
        }
    }
}
