using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.UserManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Model.Validation.Creation;
using Woozle.Services;
using Woozle.Services.UserManagement;

namespace Woozle.UnitTest.Services.UserManagement
{
    [TestClass]
    public class UserServiceTest : SessionTestBase
    {
        private Mock<IUserLogic> logicMock;
        private User modelUser;

        [TestInitialize]
        public void Initialize()
        {
            modelUser = new User() { Id = 1, FirstName = "Andreas" };
            modelUser.Language = new Language { Users = new FixupCollection<User>() { modelUser } };

            this.logicMock = new Mock<IUserLogic>();
            MappingConfiguration.Configure();
        }

        [TestMethod]
        public void FindAllTest()
        {
            IList<UserSearchResult> users = new List<UserSearchResult>()
                {
                    new UserSearchResult() {Id = 1, Firstname = "Andreas"},
                    new UserSearchResult() {Id = 2, Firstname = "Patrick"}
                };
            logicMock.Setup(n => n.Search(It.IsAny<UserSearchCriteria>(), It.IsAny<Session>())).Returns(users);

            var service = CreateUserService();
            var result = service.Get(new UsersDto());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetUsersOfCurrentMandatorTest()
        {
            var users = new List<User>()
                {
                    new User() {Id = 1},
                    new User() {Id = 2}
                };

            logicMock.Setup(n => n.GetUsersOfMandator(It.IsAny<Session>())).Returns(users);

            var service = CreateUserService();
            var result = service.Get(new UsersForDropDownDto());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }

        [TestMethod]
        public void LoadTest()
        {
            logicMock.Setup(n => n.LoadUser(1, It.IsAny<Session>())).Returns(modelUser);

            var service = CreateUserService();
            var result = service.Get(new UserDto() {Id = 1});

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.UserDto);
        }

        [TestMethod]
        public void InsertTest()
        {
            MockSave();

            var service = CreateUserService();
            var result = service.Post(new UserDto() { Id = 1 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TargetObject);
            Assert.AreEqual(1, result.TargetObject.Id);
        }

        [TestMethod]
        public void UpdateTest()
        {
            MockSave();

            var service = CreateUserService();
            var result = service.Put(new UserDto() { Id = 1 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TargetObject);
            Assert.AreEqual(1, result.TargetObject.Id);
        }

        private void MockSave()
        {
            var modelSaveResult = new SaveResult<User>() {TargetObject = modelUser};
            logicMock.Setup(n => n.Save(It.IsAny<User>(), It.IsAny<Session>())).Returns(modelSaveResult);
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
