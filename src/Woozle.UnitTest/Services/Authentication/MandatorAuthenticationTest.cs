﻿using System;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Testing;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Services.Authentication;
using Xunit;

namespace Woozle.UnitTest.Services.Authentication
{
    public class MandatorAuthenticationTest
    {
        private readonly MockHttpRequest mockedHttpRequest;
        private readonly MockHttpResponse mockedHttpResponse;

        public MandatorAuthenticationTest()
        {
            mockedHttpRequest = new MockHttpRequest();
            mockedHttpResponse = new MockHttpResponse();
            mockedHttpRequest.Container.RegisterAs<Session, IAuthSession>();
        }

        [Fact]
        public void MandatorAndUserNotSetExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(), new CredentialsAuthProvider());

            Assert.Throws<HttpError>(() => attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object()));
        }

        [Fact]
        public void UserSetButMandatorNotExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(Guid.NewGuid(), new SessionData(new User(), null), DateTime.Now), new CredentialsAuthProvider());
            Assert.Throws<HttpError>(() => attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object()));
        }

        [Fact]
        public void SessionObjectIsEmptyExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new Session(Guid.NewGuid(),null, DateTime.Now), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }

        [Fact]
        public void CouldNotCastSessionExecuteTest()
        {
            var attribute = new MandatorAuthenticateAttribute();
            AuthService.Init(() => new AuthUserSession(), new CredentialsAuthProvider());
            attribute.Execute(mockedHttpRequest, mockedHttpResponse, new object());
        }
    }
}
