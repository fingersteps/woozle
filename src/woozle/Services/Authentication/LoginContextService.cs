using AutoMapper;
using Woozle.Core.Services.Stack.Impl.Authentication;
using Woozle.Core.Services.Stack.ServiceModel.LoginContext;
using User = Woozle.Core.Services.Stack.ServiceModel.UserManagement.User;

namespace Woozle.Core.Services.Stack.Impl.LoginContext
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
        public LoginContextResult Get(ServiceModel.LoginContext.LoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Woozle.Model.User, User>(Session.SessionObject.User);
            var serviceMandator = Mapper.Map<Woozle.Model.Mandator, ServiceModel.Mandator.Mandator>(Session.SessionObject.Mandator);
            var result = new LoginContextResult() {User = serviceUser, Mandator = serviceMandator};
            return result;
        }
    }
}
