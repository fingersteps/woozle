using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Services.Authentication;
using Woozle.Services.Authority;
using Woozle.Services.UserManagement;

namespace Woozle.Services.BackendAuthentication
{
    [MandatorAuthenticate]
    [RequiredRole(Roles.Administrator)]
    public class BackendLoginContextService : AbstractService
    {
        /// <summary>
        /// Gets the context of the user which is logged in to this Session (User and Mandator)
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public BackendLoginContextResult Get(BackendLoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Model.User, User>(Session.SessionData.User);
            var serviceMandator = Mapper.Map<Model.Mandator, Mandator.Mandator>(Session.SessionData.Mandator);
            var result = new BackendLoginContextResult() { User = serviceUser, Mandator = serviceMandator };
            return result;
        }
    }
}
