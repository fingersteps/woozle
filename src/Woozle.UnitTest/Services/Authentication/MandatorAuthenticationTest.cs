using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Testing;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;

namespace Woozle.UnitTest.Services.Authentication
{
    [TestClass]
    public class MandatorAuthenticationTest
    {
        private MockHttpRequest mockedHttpRequest;
        private MockHttpResponse mockedHttpResponse;

        [TestInitialize]
        public void Initialize()
        {
            mockedHttpRequest = new MockHttpRequest();
            mockedHttpResponse = new MockHttpResponse();
            mockedHttpRequest.Container.RegisterAs<Session, IAuthSession>();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpError))]
        public void MandatorAndUserNotSetExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpError))]
        public void UserSetButMandatorNotExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(Guid.NewGuid(), new SessionData(new User(), null), DateTime.Now), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }

        [TestMethod]
        public void SessionObjectIsEmptyExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(Guid.NewGuid(),null, DateTime.Now), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }

        [TestMethod]
        public void CouldNotCastSessionExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new AuthUserSession(), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }
    }
}
