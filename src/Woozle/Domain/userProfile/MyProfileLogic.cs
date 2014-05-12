using System;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;

namespace Woozle.Domain.UserProfile
{
    public class MyProfileLogic : IMyProfileLogic
    {
        private readonly IRepository<Language> languageRepository;
        private readonly IUserLogic userLogic;
        private readonly IUserValidator userValidator;
        private readonly IHashProvider passwordHasher;

        public MyProfileLogic(IRepository<Language> languageRepository, IUserLogic userLogic, IUserValidator userValidator, IHashProvider passwordHasher)
        {
            this.languageRepository = languageRepository;
            this.userLogic = userLogic;
            this.userValidator = userValidator;
            this.passwordHasher = passwordHasher;
        }

        public void Update(string email, int languageId, SessionData sessionData)
        {
            var user = sessionData.User;
            user.Email = email;
            user.LanguageId = languageId;
            user.Language = languageRepository.FindById(languageId);
            userLogic.Save(user, sessionData);
        }

        public User ChangePassword(string oldPassword, string newPassword, SessionData sessionData)
        {
            var user = sessionData.User;
            ValidateOldPassword(user, oldPassword, sessionData);
            userValidator.ValidateUserPassword(newPassword);

            string newHash;
            string newSalt;
            passwordHasher.GetHashAndSaltString(newPassword, out newHash, out newSalt);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            return userLogic.Save(user, sessionData);
        }

        private void ValidateOldPassword(User user, string oldPassword, SessionData sessionData)
        {
            if (!passwordHasher.VerifyHashString(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("The given old password is wrong.");
            }
        }

    }
}
