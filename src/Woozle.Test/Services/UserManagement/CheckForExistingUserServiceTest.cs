using Moq;
using Woozle.Domain.UserManagement;
using Woozle.Model.SessionHandling;
using Woozle.Services;
using Woozle.Services.UserManagement;
using Xunit;

namespace Woozle.Test.Services.UserManagement
{
    public class CheckForExistingUserServiceTest : SessionTestBase
    {
                private Mock<IUserLogic> logicMock;
        private Model.User modelUser;

        public CheckForExistingUserServiceTest()
        {
            modelUser = new Model.User { Id = 1, FirstName = "Andreas" };

            this.logicMock = new Mock<IUserLogic>();
            MappingConfiguration.Configure();
        }

        [Fact]
        public void CheckUsernameWhichAlreadyExists()
        {
            logicMock.Setup(n => n.GetUserByUsername("sha", It.IsAny<SessionData>())).Returns(modelUser);

            var result = CreateService().Get(new UserAlreadyExists() { Username = "sha" });

            Assert.True(result);
        }


        [Fact]
        public void CheckUsernameWhichDoesNotAlreadyExist()
        {
            var result = CreateService().Get(new UserAlreadyExists() { Username = "newUser" });

            Assert.False(result);
        }

        private CheckForExistingUserService CreateService()
        {
            var requestContextMock = this.GetFakeRequestContext();

            var locationService = new CheckForExistingUserService(logicMock.Object)
            {
                RequestContext =
                    requestContextMock
            };

            return locationService;
        }
    }
}
