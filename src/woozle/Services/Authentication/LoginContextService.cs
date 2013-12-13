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
        public LoginContextResultDto Get(LoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Model.User, UserDto>(Session.SessionObject.User);
            var serviceMandator = Mapper.Map<Model.Mandator, Mandator.MandatorDto>(Session.SessionObject.Mandator);
            var result = new LoginContextResultDto {UserDto = serviceUser, MandatorDto = serviceMandator};
            return result;
        }
    }
}
