using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AutoMapper;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authority;
using Woozle.Settings;

namespace Woozle.Services.Authentication
{
    public class WoozleAuthRepository : IUserAuthRepository
    {
        //http://stackoverflow.com/questions/3588623/c-sharp-regex-for-a-username-with-a-few-restrictions
        public Regex ValidUserNameRegEx = new Regex(@"^(?=.{3,15}$)([A-Za-z0-9][._-]?)*$", RegexOptions.Compiled);

        public Regex ValidPasswordRegex = new Regex(@"^(.{5,20}$)", RegexOptions.Compiled);

        private readonly IUserLogic userLogic;
        private readonly IWoozleSettings woozleSettings;
        private readonly IRegistrationSettings registrationSettings;
        private readonly IGetRolesLogic getRolesLogic;
        private readonly IHashProvider passwordHasher;

        public WoozleAuthRepository(IUserLogic userLogic, IWoozleSettings woozleSettings, IRegistrationSettings registrationSettings, IGetRolesLogic getRolesLogic, IHashProvider passwordHasher)
        {
            this.userLogic = userLogic;
            this.woozleSettings = woozleSettings;
            this.registrationSettings = registrationSettings;
            this.getRolesLogic = getRolesLogic;
            this.passwordHasher = passwordHasher;
        }

        private void ValidateNewUser(UserAuth newUser, string password)
        {
            newUser.ThrowIfNull("updatedUser");
            password.ThrowIfNullOrEmpty("password");

            if (newUser.UserName.IsNullOrEmpty() && newUser.Email.IsNullOrEmpty())
                throw new ArgumentNullException("UserName or Email is required");

            if (!newUser.UserName.IsNullOrEmpty())
            {
                if (!ValidUserNameRegEx.IsMatch(newUser.UserName))
                {
                    throw new ArgumentException("UserName contains invalid characters", "UserName");
                }
            }

            if (!ValidPasswordRegex.IsMatch(password))
            {
                throw new ArgumentException("Password contains invalid characters", "Password");
            }
        }

        public UserAuth CreateUserAuth(UserAuth newUser, string password)
        {
            ValidateNewUser(newUser, password);

            AssertNoExistingUser(newUser);

            string salt;
            string hash;
            passwordHasher.GetHashAndSaltString(password, out hash, out salt);

            newUser.PasswordHash = hash;
            newUser.Salt = salt;

            newUser.CreatedDate = DateTime.UtcNow;
            newUser.ModifiedDate = newUser.CreatedDate;

            var user = Mapper.Map<UserAuth, User>(newUser);

            user.PersistanceState = PState.Added;
            user.FlagActiveStatusId = registrationSettings.DefaultFlagActiveStatus.Id;
            user.LanguageId = registrationSettings.DefaultLanguage.Id;

            var sessionData = CreateSessionData();
            user.UserMandatorRoles.Add(new UserMandatorRole()
            {
                PersistanceState = PState.Added,
                MandatorRoleId = getRolesLogic.GetMandatorRoleByName(Roles.User, sessionData).Id
            });

            userLogic.Save(user, sessionData);
            return newUser;
        }

        private SessionData CreateSessionData()
        {
           return new SessionData(new User() {Username = "system"}, woozleSettings.DefaultMandator);
        }

        private void AssertNoExistingUser(UserAuth newUser, UserAuth exceptForExistingUser = null)
        {
            if (newUser.UserName != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.UserName);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException("User {0} already exists".Fmt(newUser.UserName));
            }
            if (newUser.Email != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.Email);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException("Email {0} already exists".Fmt(newUser.Email));
            }
        }

        public UserAuth UpdateUserAuth(UserAuth existingUser, UserAuth updatedUser, string password)
        {
            //TODO: Uncomment, Test and check the following code as soon update of a user is needed
            return updatedUser;
            //ValidateNewUser(updatedUser, password);

            //AssertNoExistingUser(updatedUser, existingUser);

            //var hash = existingUser.PasswordHash;
            //var salt = existingUser.Salt;
            //if (password != null)
            //{
            //    passwordHasher.GetHashAndSaltString(password, out hash, out salt);
            //}
            //// If either one changes the digest hash has to be recalculated
            //var digestHash = existingUser.DigestHa1Hash;
            //if (password != null || existingUser.UserName != updatedUser.UserName)
            //{
            //    var digestHelper = new DigestAuthFunctions();
            //    digestHash = digestHelper.CreateHa1(updatedUser.UserName, DigestAuthProvider.Realm, password);
            //}
            //updatedUser.Id = existingUser.Id;
            //updatedUser.PasswordHash = hash;
            //updatedUser.Salt = salt;
            //updatedUser.DigestHa1Hash = digestHash;
            //updatedUser.CreatedDate = existingUser.CreatedDate;
            //updatedUser.ModifiedDate = DateTime.UtcNow;

            //var user = Mapper.Map<UserAuth, User>(updatedUser);
            //var sessionData = CreateSessionData();
            //userLogic.Save(user, sessionData);
            //return updatedUser;
        }

        public UserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            var user = userLogic.GetUserByUsername(userNameOrEmail, CreateSessionData());
            return Mapper.Map<User, UserAuth>(user);
        }

        public bool TryAuthenticate(string userName, string password, out UserAuth userAuth)
        {
            userAuth = GetUserAuthByUserName(userName);
            if (userAuth == null) return false;

            if (passwordHasher.VerifyHashString(password, userAuth.PasswordHash, userAuth.Salt))
            {
                return true;
            }

            userAuth = null;
            return false;
        }

        public bool TryAuthenticate(Dictionary<string,string> digestHeaders, string PrivateKey, int NonceTimeOut, string sequence, out UserAuth userAuth)
        {
            userAuth = GetUserAuthByUserName(digestHeaders["username"]);
            if (userAuth == null) return false;

            var digestHelper = new DigestAuthFunctions();
            if (digestHelper.ValidateResponse(digestHeaders,PrivateKey,NonceTimeOut,userAuth.DigestHa1Hash,sequence))
            {
                return true;
            }
            userAuth = null;
            return false;
        }

        public void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
        }

        public UserAuth GetUserAuth(string userAuthId)
        {
            var user = userLogic.FindUserById(int.Parse(userAuthId));
            return Mapper.Map<User, UserAuth>(user);
        }

        public void SaveUserAuth(IAuthSession authSession)
        {
            //Can be used to update information like "last login date".
        }

        public void SaveUserAuth(UserAuth userAuth)
        {
        }

        public List<UserOAuthProvider> GetUserOAuthProviders(string userAuthId)
        {
            return null;
        }

        public UserAuth GetUserAuth(IAuthSession authSession, IOAuthTokens tokens)
        {
            if (!authSession.UserAuthId.IsNullOrEmpty())
            {
                var userAuth = GetUserAuth(authSession.UserAuthId);
                if (userAuth != null) return userAuth;
            }
            if (!authSession.UserAuthName.IsNullOrEmpty())
            {
                var userAuth = GetUserAuthByUserName(authSession.UserAuthName);
                if (userAuth != null) return userAuth;
            }
            return null;
        }

        public string CreateOrMergeAuthSession(IAuthSession authSession, IOAuthTokens tokens)
        {
            var userAuth = GetUserAuth(authSession, tokens) ?? new UserAuth();
            return userAuth.Id.ToString(CultureInfo.InvariantCulture);
        }
    }
}