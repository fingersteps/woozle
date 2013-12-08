﻿using System.Collections.Generic;
using AutoMapper;
using Woozle.Core.BusinessLogic.UserManagement;
using Woozle.Core.Model.UserSearch;
using Woozle.Core.Services.Stack.Impl.Authentication;
using Woozle.Core.Services.Stack.ServiceModel;
using Woozle.Core.Services.Stack.ServiceModel.UserManagement;
using UserSearchResult = Woozle.Core.Model.UserSearch.UserSearchResult;

namespace Woozle.Core.Services.Stack.Impl.UserManagement
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
        public IList<UserSearchResult> Get(Users request)
        {
            var criteria = Mapper.Map<Users, UserSearchCriteria>(request);
            var result = logic.Search(criteria, Session);
            return Mapper.Map<IList<UserSearchResult>, List<UserSearchResult>>(result);
        }

        /// <summary>
        /// Gets all users of the mandator of the currently logged in user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public List<User> Get(UsersForDropDown request)
        {
            var result = logic.GetUsersOfMandator(Session);
            return Mapper.Map<IList<Woozle.Model.User>, List<User>>(result);
        }

        [ExceptionCatcher]
        public UserResponse Get(User request)
        {
            if (request.Id == 0)
            {
                return new UserResponse
                           {
                               User = Mapper.Map <Woozle.Model.User,User>(this.Session.SessionObject.User)
                           };
            }

            var result = logic.LoadUser(request.Id, Session);
            var response = new UserResponse() {User = Mapper.Map<Woozle.Model.User, User>(result)};
            return response;
        }

        [ExceptionCatcher]
        public UserResponse Get()
        {
            var result = logic.LoadUser(this.Session.SessionObject.User.Id, Session);
            var response = new UserResponse
                               {
                                   User = Mapper.Map<Woozle.Model.User, User>(result)
                               };

            return response;
        }

        /// <summary>
        /// Inserts a given object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<User> Post(User user)
        {
            return Save(user);
        }

        /// <summary>
        /// Updates a given object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public SaveResult<User> Put(User user)
        {
            return Save(user);
        }

        private SaveResult<User> Save(User user)
        {
            var saveResult = this.logic.Save(Mapper.Map<User, Woozle.Model.User>(user), Session);
            return Mapper.Map<Model.Validation.Creation.ISaveResult<Woozle.Model.User>, SaveResult<User>>(saveResult);
        }

        [ExceptionCatcher]
        public object Delete(User user)
        {
            this.logic.Delete(user.Id, Session);
            return null;
        }
    }
}