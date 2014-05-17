using Woozle.Domain.UserManagement;

namespace Woozle.Services.UserManagement
{
    public class CheckForExistingUserService : MandatorAuthenticatedService
    {
        private readonly IUserLogic logic;

        public CheckForExistingUserService(IUserLogic logic)
        {
            this.logic = logic;
        }

        [ExceptionCatcher]
        public bool Get(UserAlreadyExists request)
        {
            var result = logic.GetUserByUsername(request.Username, Session.SessionData);
            return result != null;
        }
    }
}
