﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.UserManagement;
using Xunit;
using User = Woozle.Services.UserManagement.User;
using UserSearchResult = Woozle.Model.UserSearch.UserSearchResult;

namespace Woozle.UnitTest.Services.UserManagement
{
    public class UserServiceTest : SessionTestBase
    {
        private Mock<IUserLogic> logicMock;
        private Model.User modelUser;

        public UserServiceTest()
        {
            modelUser = new Model.User { Id = 1, FirstName = "Andreas" };
            modelUser.Language = new Language { Users = new ObservableCollection<Model.User>() { modelUser } };

            this.logicMock = new Mock<IUserLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void FindAllTest()
        {
            IList<UserSearchResult> users = new List<UserSearchResult>()
                {
                    new UserSearchResult() {Id = 1, Firstname = "Andreas"},
                    new UserSearchResult() {Id = 2, Firstname = "Patrick"}
                };
            logicMock.Setup(n => n.Search(It.IsAny<UserSearchCriteria>(), It.IsAny<Session>())).Returns(users);

            var service = CreateUserService();
            var result = service.Get(new Users());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetUsersOfCurrentMandatorTest()
        {
            var users = new List<Model.User>()
                {
                    new Model.User() {Id = 1},
                    new Model.User() {Id = 2}
                };

            logicMock.Setup(n => n.GetUsersOfMandator(It.IsAny<Session>())).Returns(users);

            var service = CreateUserService();
            var result = service.Get(new UsersForDropDownDto());

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
        }

        [Fact]
        public void LoadTest()
        {
            logicMock.Setup(n => n.LoadUser(1, It.IsAny<Session>())).Returns(modelUser);

            var service = CreateUserService();
            var result = service.Get(new User() {Id = 1});

            Assert.NotNull(result);
            Assert.NotNull(result.User);
        }

        [Fact]
        public void InsertTest()
        {
            MockSave();

            var service = CreateUserService();
            var result = service.Post(new User() { Id = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.TargetObject);
            Assert.Equal(1, result.TargetObject.Id);
        }

        [Fact]
        public void UpdateTest()
        {
            MockSave();

            var service = CreateUserService();
            var result = service.Put(new User() { Id = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.TargetObject);
            Assert.Equal(1, result.TargetObject.Id);
        }

        private void MockSave()
        {
            var modelSaveResult = new SaveResult<Model.User>() {TargetObject = modelUser};
            logicMock.Setup(n => n.Save(It.IsAny<Model.User>(), It.IsAny<Session>())).Returns(modelSaveResult);
        }

        private UserService CreateUserService()
        {
            var requestContextMock = this.GetRequestContextMock();

            var locationService = new UserService(logicMock.Object)
            {
                RequestContext =
                    requestContextMock.Object
            };

            return locationService;
        }
    }
}
