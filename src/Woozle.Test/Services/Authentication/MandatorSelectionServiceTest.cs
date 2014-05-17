using System;
using System.Collections.Generic;
using Moq;
using Woozle.Domain.Authentication;
using Woozle.Model;
using Woozle.Model.Authentication;
using Woozle.Model.SessionHandling;
using Woozle.Model.UserSearch;
using Woozle.Services;
using Woozle.Services.Authentication;
using Woozle.Services.Mandator;
using Xunit;

namespace Woozle.Test.Services.Authentication
{
    public class MandatorSelectionServiceTest : SessionTestBase
    {
        private const string Username = "test-user";

        private readonly Mock<IAuthenticationLogic> mockedAuthenticationLogic;

        private readonly MandatorSelectionService mandatorSelectionService;
        private readonly User user;
        private Woozle.Model.Mandator mandator;


        public MandatorSelectionServiceTest()
        {
            MappingConfiguration.Configure();

            user = new User() { Username = Username};
            mandator = new Woozle.Model.Mandator();
            var sessionObject = new SessionData(user, mandator);
            var session = new Session(sessionObject);

            this.mockedAuthenticationLogic = new Mock<IAuthenticationLogic>();

            this.mandatorSelectionService = new MandatorSelectionService(mockedAuthenticationLogic.Object)
                                                {
                                                    RequestContext = GetFakeRequestContext(session)
                                                };
        }

        [Fact]
        public void MandatorSelectionService_GetSelectableMandatorsTest()
        {
            const string NameMandator1 = "test-mandator-1";
            const string NameMandator2 = "test-mandator-2";

            var suggestedMandators = new List<Model.Mandator>
                                         {
                                             new Model.Mandator {Name = NameMandator1},
                                             new Model.Mandator {Name = NameMandator2}
                                         };

            this.mockedAuthenticationLogic.Setup(n => n.GetLoginUser(Username))
                                          .Returns(new UserSearchForLoginResult
                                                         {
                                                             Mandators = suggestedMandators
                                                         });

            var mandators = this.mandatorSelectionService.Get(new MandatorsForSelection());

            Assert.Equal(2, mandators.Count);
            Assert.Equal(NameMandator1, mandators[0].Name);
            Assert.Equal(NameMandator2, mandators[1].Name);
        }

        [Fact]
        public void MandatorSelectionService_SelectSuccessfullyAMandatorTest()
        {
            const string MandatorName = "selected-mandator";

            user.Username = Username;

            this.mockedAuthenticationLogic.Setup(n => n.LoginMandator(Username, It.IsAny<Model.Mandator>()))
                .Returns(new LoginResult(new SessionData(user, new Model.Mandator
                                                                   {
                                                                       Name = MandatorName
                                                                   }), true));

            var selectedMandator = new Woozle.Services.Mandator.Mandator
            {
                Name = MandatorName
            };

            var result = this.mandatorSelectionService.Post(new MandatorSelect
                                                   {
                                                       SelectedMandator = selectedMandator
                                                   });
            Assert.True(result);
        }

        [Fact]
        public void MandatorSelectionService_MandatorSelectionIsNotSuccessfullyTest()
        {
            const string MandatorName = "selected-mandator";

            this.mockedAuthenticationLogic.Setup(n => n.LoginMandator(Username, It.IsAny<Model.Mandator>()))
                .Returns(new LoginResult(new SessionData(user,null), false));

            var selectedMandator = new Woozle.Services.Mandator.Mandator
            {
                Name = MandatorName
            };
            var result = this.mandatorSelectionService.Post(new MandatorSelect
            {
                SelectedMandator = selectedMandator
            });

            Assert.False(result);
        }
    }
}
