﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;

namespace Woozle.Services.Authentication
{
    public class WoozleAuthRepository : IUserAuthRepository
    {
        //http://stackoverflow.com/questions/3588623/c-sharp-regex-for-a-username-with-a-few-restrictions
        public Regex ValidUserNameRegEx = new Regex(@"^(?=.{3,15}$)([A-Za-z0-9][._-]?)*$", RegexOptions.Compiled);

        private readonly IUserLogic userLogic;
        private readonly IHashProvider passwordHasher;

        public WoozleAuthRepository(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
            this.passwordHasher = new SaltedHash();
        }

        private void ValidateNewUser(UserAuth newUser, string password)
        {
            newUser.ThrowIfNull("newUser");
            password.ThrowIfNullOrEmpty("password");

            if (newUser.UserName.IsNullOrEmpty() && newUser.Email.IsNullOrEmpty())
                throw new ArgumentNullException("UserName or Email is required");

            if (!newUser.UserName.IsNullOrEmpty())
            {
                if (!ValidUserNameRegEx.IsMatch(newUser.UserName))
                    throw new ArgumentException("UserName contains invalid characters", "UserName");
            }
        }

        public UserAuth CreateUserAuth(UserAuth newUser, string password)
        {
            ValidateNewUser(newUser, password);

            AssertNoExistingUser(newUser);

            //TODO: Check password hashing
            //string salt;
            // string hash;
            // passwordHasher.GetHashAndSaltString(password, out hash, out salt);

            //var digestHelper = new DigestAuthFunctions();
            //newUser.DigestHa1Hash = digestHelper.CreateHa1(newUser.UserName, DigestAuthProvider.Realm, password);
            //newUser.PasswordHash = hash;
            //newUser.Salt = salt;

            newUser.PasswordHash = password;
            newUser.CreatedDate = DateTime.UtcNow;
            newUser.ModifiedDate = newUser.CreatedDate;

            return SaveUser(newUser);
        }

        private UserAuth SaveUser(UserAuth newUser)
        {
            //TODO: Add a "default mandator" in Woozle which can be set in configuration of a specific AppHost
            var sessionData = CreateSessionData(1);
            var user = Mapper.Map<UserAuth, User>(newUser);

            //TODO: Fill with default values!
            user.FlagActiveStatusId = 1;
            user.LanguageId = 1;

            userLogic.Save(user, sessionData);
            return newUser;
        }

        private static SessionData CreateSessionData(int? mandatorId)
        {
            if (!mandatorId.HasValue)
            {
                throw new ArgumentException(
                    "The given mandator is not valid. Please fill the mandator to which the user needs to be registered into the 'RefId' field.");
            }
           return new SessionData(new User() {Username = "system"}, new Model.Mandator() {Id = mandatorId.Value});
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

        public UserAuth UpdateUserAuth(UserAuth existingUser, UserAuth newUser, string password)
        {
            ValidateNewUser(newUser, password);

            AssertNoExistingUser(newUser, existingUser);

            var hash = existingUser.PasswordHash;
            var salt = existingUser.Salt;
            if (password != null)
            {
                passwordHasher.GetHashAndSaltString(password, out hash, out salt);
            }
            // If either one changes the digest hash has to be recalculated
            var digestHash = existingUser.DigestHa1Hash;
            if (password != null || existingUser.UserName != newUser.UserName)
            {
                var digestHelper = new DigestAuthFunctions();
                digestHash = digestHelper.CreateHa1(newUser.UserName, DigestAuthProvider.Realm, password);
            }
            newUser.Id = existingUser.Id;
            newUser.PasswordHash = hash;
            newUser.Salt = salt;
            newUser.DigestHa1Hash = digestHash;
            newUser.CreatedDate = existingUser.CreatedDate;
            newUser.ModifiedDate = DateTime.UtcNow;

            return SaveUser(newUser);
        }

        public UserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            var sessionData = CreateSessionData(0);
            var user = userLogic.GetUserByUsername(userNameOrEmail, sessionData);
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