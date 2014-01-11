using AutoMapper;
using Woozle.Services.UserManagement;

namespace Woozle.Services.Authentication
{
    [MandatorAuthenticate]
    public class LoginContextService : AbstractService
    {
        /// <summary>
        /// Gets the context of the user which is logged in to this Session (User and Mandator)
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public LoginContextResult Get(LoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Model.User, User>(Session.SessionObject.User);
            var serviceMandator = Mapper.Map<Model.Mandator, Mandator.Mandator>(Session.SessionObject.Mandator);
            var result = new LoginContextResult {User = serviceUser, Mandator = serviceMandator};
            return result;
        }
    }
}
