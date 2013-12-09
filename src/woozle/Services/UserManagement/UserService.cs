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
        public IList<UserSearchResult> Get(UsersDto request)
        {
            var criteria = Mapper.Map<UsersDto, UserSearchCriteria>(request);
            var result = logic.Search(criteria, Session);
            return Mapper.Map<IList<UserSearchResult>, List<UserSearchResult>>(result);
        }

        /// <summary>
        /// Gets all users of the mandator of the currently logged in user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<UserDto> Get(UsersForDropDownDto request)
        {
            var result = logic.GetUsersOfMandator(Session);
            return Mapper.Map<IList<Model.User>, List<UserDto>>(result);
        }

        [ExceptionCatcher]
        public UserResponse Get(UserDto request)
        {
            if (request.Id == 0)
            {
                return new UserResponse
                           {
                               UserDto = Mapper.Map <Model.User,UserDto>(this.Session.SessionObject.User)
                           };
            }

            var result = logic.LoadUser(request.Id, Session);
            var response = new UserResponse() {UserDto = Mapper.Map<Model.User, UserDto>(result)};
            return response;
        }

        [ExceptionCatcher]
        public UserResponse Get()
        {
            var result = logic.LoadUser(this.Session.SessionObject.User.Id, Session);
            var response = new UserResponse
                               {
                                   UserDto = Mapper.Map<Model.User, UserDto>(result)
                               };

            return response;
        }

        /// <summary>
        /// Inserts a given object
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResultDto<UserDto> Post(UserDto userDto)
        {
            return Save(userDto);
        }

        /// <summary>
        /// Updates a given object
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResultDto<UserDto> Put(UserDto userDto)
        {
            return Save(userDto);
        }

        private SaveResultDto<UserDto> Save(UserDto userDto)
        {
            var saveResult = logic.Save(Mapper.Map<UserDto, Model.User>(userDto), Session);
            return Mapper.Map<ISaveResult<Model.User>, SaveResultDto<UserDto>>(saveResult);
        }

        [ExceptionCatcher]
        public object Delete(UserDto userDto)
        {
            logic.Delete(userDto.Id, Session);
            return null;
        }
    }
}
