using System.Collections.Generic;
using AutoMapper;
using Woozle.Domain.UserManagement;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Services.Authentication;

namespace Woozle.Services.UserManagement
{
    [MandatorAuthenticate]
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
        public List<UserDto> Get(UsersForDropDown request)
        {
            var result = logic.GetUsersOfMandator(Session);
            return Mapper.Map<IList<Woozle.Model.User>, List<UserDto>>(result);
        }

        [ExceptionCatcher]
        public UserResponse Get(UserDto request)
        {
            if (request.Id == 0)
            {
                return new UserResponse
                           {
                               UserDto = Mapper.Map <Woozle.Model.User,UserDto>(this.Session.SessionObject.User)
                           };
            }

            var result = logic.LoadUser(request.Id, Session);
            var response = new UserResponse() {UserDto = Mapper.Map<Woozle.Model.User, UserDto>(result)};
            return response;
        }

        [ExceptionCatcher]
        public UserResponse Get()
        {
            var result = logic.LoadUser(this.Session.SessionObject.User.Id, Session);
            var response = new UserResponse
                               {
                                   UserDto = Mapper.Map<Woozle.Model.User, UserDto>(result)
                               };

            return response;
        }

        /// <summary>
        /// Inserts a given object
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<UserDto> Post(UserDto userDto)
        {
            return Save(userDto);
        }

        /// <summary>
        /// Updates a given object
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<UserDto> Put(UserDto userDto)
        {
            return Save(userDto);
        }

        private SaveResult<UserDto> Save(UserDto userDto)
        {
            var saveResult = this.logic.Save(Mapper.Map<UserDto, Woozle.Model.User>(userDto), Session);
            return Mapper.Map<ISaveResult<Woozle.Model.User>, SaveResult<UserDto>>(saveResult);
        }

        [ExceptionCatcher]
        public object Delete(UserDto userDto)
        {
            this.logic.Delete(userDto.Id, Session);
            return null;
        }
    }
}
