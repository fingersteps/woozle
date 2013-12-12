using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Services;
using Woozle.Services.Authentication;
using Woozle.Services.Mandator;

namespace Woozle.UnitTest.Services.Authentication
{
    [TestClass]
    public class MandatorSelectionServiceTest : SessionTestBase
    {
        private const string Username = "test-user";
        private const string Password = "test-passwordr";

        private Mock<IAuthenticationLogic> mockedAuthenticationLogic;

        private MandatorSelectionService mandatorSelectionService;
        private User user;
        private Woozle.Model.Mandator mandator;


        [TestInitialize]
        public void Initialize()
        {
            MappingConfiguration.Configure();

            user = new User() { Username = Username, Password = Password };
            mandator = new Woozle.Model.Mandator();
            var sessionObject = new SessionData(user, mandator);
            var session = new Session(Guid.NewGuid(), sessionObject, DateTime.Now);

            this.mockedAuthenticationLogic = new Mock<IAuthenticationLogic>();

            this.mandatorSelectionService = new MandatorSelectionService(mockedAuthenticationLogic.Object)
                                                {
                                                    RequestContext = GetRequestContextMock(session)
                                                };
        }

        [TestMethod]
        public void MandatorSelectionService_GetSelectableMandatorsTest()
        {
            const string NameMandator1 = "test-mandator-1";
            const string NameMandator2 = "test-mandator-2";

            var suggestedMandators = new List<Model.Mandator>
                                         {
                                             new Model.Mandator {Name = NameMandator1},
                                             new Model.Mandator {Name = NameMandator2}
                                         };

            this.mockedAuthenticationLogic.Setup(n => n.GetLoginUser(Username, Password))
                                          .Returns(new UserSearchForLoginResult
                                                         {
                                                             Mandators = suggestedMandators
                                                         });

            var mandators = this.mandatorSelectionService.Get(new MandatorsForSelectionDto());

            Assert.AreEqual(2, mandators.Count);
            Assert.AreEqual(NameMandator1, mandators[0].Name);
            Assert.AreEqual(NameMandator2, mandators[1].Name);
        }

        [TestMethod]
        public void MandatorSelectionService_SelectSuccessfullyAMandatorTest()
        {
            const string MandatorName = "selected-mandator";

            user.Username = Username;
            user.Password = Password;

            this.mockedAuthenticationLogic.Setup(n => n.Login(
                It.Is<LoginRequest>(
                    b => b.Username == Username && b.Password == Password && b.Mandator.Name == MandatorName)))
                .Returns(new LoginResult(new SessionData(user, new Model.Mandator
                                                                   {
                                                                       Name = MandatorName
                                                                   }), true));

            var selectedMandator = new MandatorDto
            {
                Name = MandatorName
            };

            var result = this.mandatorSelectionService.Post(new MandatorSelectDto
                                                   {
                                                       SelectedMandatorDto = selectedMandator
                                                   });
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MandatorSelectionService_MandatorSelectionIsNotSuccessfullyTest()
        {
            const string MandatorName = "selected-mandator";

            this.mockedAuthenticationLogic.Setup(n => n.Login(
                It.Is<LoginRequest>(
                    b => b.Username == Username && b.Password == Password && b.Mandator.Name == MandatorName)))
                .Returns(new LoginResult(new SessionData(user,null), false));

            var selectedMandator = new MandatorDto
            {
                Name = MandatorName
            };
            var result = this.mandatorSelectionService.Post(new MandatorSelectDto
            {
                SelectedMandatorDto = selectedMandator
            });

            Assert.IsFalse(result);
        }
    }
}
