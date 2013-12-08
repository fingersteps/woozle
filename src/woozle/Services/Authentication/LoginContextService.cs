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
        public LoginContextResult Get(Woozle.Services.Authentication.LoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Woozle.Model.User, UserDto>(Session.SessionObject.User);
            var serviceMandator = Mapper.Map<Woozle.Model.Mandator, Mandator.Mandator>(Session.SessionObject.Mandator);
            var result = new LoginContextResult() {UserDto = serviceUser, Mandator = serviceMandator};
            return result;
        }
    }
}
