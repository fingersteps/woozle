using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IUserLogic userLogic;
        private readonly IWoozleSettings woozleSettings;
        private readonly IRegistrationSettings registrationSettings;
        private readonly IGetRolesLogic getRolesLogic;
        private readonly IHashProvider passwordHasher;
        private readonly IUserValidator userValidator;

        public WoozleAuthRepository(IUserLogic userLogic, IWoozleSettings woozleSettings, 
            IRegistrationSettings registrationSettings, IGetRolesLogic getRolesLogic, 
            IHashProvider passwordHasher, IUserValidator userValidator)
        {
            this.userLogic = userLogic;
            this.woozleSettings = woozleSettings;
            this.registrationSettings = registrationSettings;
            this.getRolesLogic = getRolesLogic;
            this.passwordHasher = passwordHasher;
            this.userValidator = userValidator;
        }

        public UserAuth CreateUserAuth(UserAuth newUserAuth, string password)
        {
            userValidator.ValidateNewUser(newUserAuth.UserName, newUserAuth.Email);
            userValidator.ValidateUserPassword(password);

            AssertNoExistingUser(newUserAuth);

            string salt;
            string hash;
            passwordHasher.GetHashAndSaltString(password, out hash, out salt);

            newUserAuth.PasswordHash = hash;
            newUserAuth.Salt = salt;

            var user = Mapper.Map<UserAuth, User>(newUserAuth);
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
            return newUserAuth;
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
            throw new NotImplementedException();
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