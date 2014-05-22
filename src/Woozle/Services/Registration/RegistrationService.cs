using System.Globalization;
using AutoMapper;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Domain.Registration;
using Woozle.Model;

namespace Woozle.Services.Registration
{
    [DefaultRequest(typeof(Registration))]
    public class RegistrationService : Service
    {
        private readonly IRegistrationLogic registrationLogic;

        public RegistrationService(IRegistrationLogic registrationLogic)
        {
            this.registrationLogic = registrationLogic;
        }

        /// <summary>
        /// Create new Registration
        /// </summary>
        public object Post(Registration request)
        {
            var session = this.GetSession();
            var newUserAuth = ToUserAuth(request);

            var newUser = Mapper.Map<UserAuth, User>(newUserAuth);
            var user = this.registrationLogic.RegisterUser(newUser, request.Password);

            session.OnRegistered(this);

            var response = new ServiceStack.ServiceInterface.Auth.RegistrationResponse
            {
                UserId = user.Id.ToString(CultureInfo.InvariantCulture),
                ReferrerUrl = request.Continue
            };
            
            return response;
        }

        private UserAuth ToUserAuth(Registration request)
        {
            var to = request.TranslateTo<UserAuth>();
            to.PrimaryEmail = request.Email;
            to.Language = request.LanguageCode;
            return to;
        }
    }
}