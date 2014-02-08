using System.Collections.Generic;
using AutoMapper;
using ServiceStack.ServiceInterface;
using Woozle.Domain.UserManagement;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Services.Authentication;
using Woozle.Services.Authority;

namespace Woozle.Services.UserManagement
{
    [MandatorAuthenticate]
    [RequiredRole(Roles.Administrator)]
    public class UserService : AbstractService
    {
        private readonly IUserLogic logic;

        public UserService(IUserLogic logic)
        {
            this.logic = logic;
        }

        [ExceptionCatcher]
        public IList<Model.UserSearch.UserSearchResult> Get(Users request)
        {
            var criteria = Mapper.Map<Users, UserSearchCriteria>(request);
            var result = logic.Search(criteria, Session);
            return Mapper.Map<IList<Model.UserSearch.UserSearchResult>, List<Model.UserSearch.UserSearchResult>>(result);
        }

        /// <summary>
        /// Gets all users of the mandator of the currently logged in user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<User> Get(UsersForDropDownDto request)
        {
            var result = logic.GetUsersOfMandator(Session);
            return Mapper.Map<IList<Model.User>, List<User>>(result);
        }

        [ExceptionCatcher]
        public UserResponse Get(User request)
        {
            var result = logic.LoadUser(request.Id, Session);
            var response = new UserResponse() {User = Mapper.Map<Model.User, User>(result)};
            return response;
        }

        /// <summary>
        /// Inserts a given object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResultDto<User> Post(User user)
        {
            return Save(user);
        }

        /// <summary>
        /// Updates a given object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResultDto<User> Put(User user)
        {
            return Save(user);
        }

        private SaveResultDto<User> Save(User user)
        {
            var saveResult = logic.Save(Mapper.Map<User, Model.User>(user), Session);
            return Mapper.Map<ISaveResult<Model.User>, SaveResultDto<User>>(saveResult);
        }

        [ExceptionCatcher]
        public object Delete(User user)
        {
            logic.Delete(user.Id, Session);
            return null;
        }
    }
}
