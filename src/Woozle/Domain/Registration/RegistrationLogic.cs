using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using Woozle.Domain.Authentication;
using Woozle.Domain.Authority;
using Woozle.Domain.Location;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authority;
using Woozle.Settings;

namespace Woozle.Domain.Registration
{
    public class RegistrationLogic : IRegistrationLogic
    {
        private readonly IUserLogic userLogic;
        private readonly IWoozleSettings woozleSettings;
        private readonly IRegistrationSettings registrationSettings;
        private readonly IGetRolesLogic getRolesLogic;
        private readonly IHashProvider passwordHasher;
        private readonly IUserValidator userValidator;
        private readonly ILocationLogic locationLogic;

        public RegistrationLogic(IUserLogic userLogic, IWoozleSettings woozleSettings, 
            IRegistrationSettings registrationSettings, IGetRolesLogic getRolesLogic, 
            IHashProvider passwordHasher, IUserValidator userValidator, ILocationLogic locationLogic)
        {
            this.userLogic = userLogic;
            this.woozleSettings = woozleSettings;
            this.registrationSettings = registrationSettings;
            this.getRolesLogic = getRolesLogic;
            this.passwordHasher = passwordHasher;
            this.userValidator = userValidator;
            this.locationLogic = locationLogic;
        }

        public User RegisterUser(User user, string password)
        {
            userValidator.ValidateNewUser(user.Username, user.Email);
            userValidator.ValidateUserPassword(password);

            AssertNoExistingUser(user);

            string salt;
            string hash;
            passwordHasher.GetHashAndSaltString(password, out hash, out salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            user.PersistanceState = PState.Added;
            user.FlagActiveStatusId = registrationSettings.DefaultFlagActiveStatus.Id;
            SetLanguageId(user);

            var sessionData = CreateSessionData();
            user.UserMandatorRoles.Add(new UserMandatorRole()
            {
                PersistanceState = PState.Added,
                MandatorRoleId = getRolesLogic.GetMandatorRoleByName(Roles.User, sessionData).Id
            });

            var savedUser = userLogic.Save(user, sessionData);
            return savedUser;
        }

        private void SetLanguageId(User user)
        {
            int languageId;
            if (user.Language.Code.IsNullOrEmpty())
            {
                languageId = woozleSettings.DefaultLanguage.Id;
            }
            else
            {
                var language = locationLogic.LoadLanguage(user.Language.Code);
                languageId = language.Id;
            }

            //Ecplicity set language to null to avoid inserting a new language
            user.Language = null;
            user.LanguageId = languageId;
        }

        private SessionData CreateSessionData()
        {
           return new SessionData(new User() {Username = "system"}, woozleSettings.DefaultMandator);
        }

        private void AssertNoExistingUser(User newUser, UserAuth exceptForExistingUser = null)
        {
            if (newUser.Username != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.Username);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException("User {0} already exists".Fmt(newUser.Username));
            }
            if (newUser.Email != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.Email);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                    throw new ArgumentException("Email {0} already exists".Fmt(newUser.Email));
            }
        }

        public UserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            var user = userLogic.GetUserByUsername(userNameOrEmail, CreateSessionData());
            return Mapper.Map<User, UserAuth>(user);
        }

        public UserAuth GetUserAuth(string userAuthId)
        {
            var user = userLogic.FindUserById(int.Parse(userAuthId));
            return Mapper.Map<User, UserAuth>(user);
        }
    }
}