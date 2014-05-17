﻿using AutoMapper;
using PostSharp.Aspects;
using ServiceStack.ServiceInterface;
using Woozle.Domain.Location;
using Woozle.Model.SessionHandling;
using Woozle.Services.Mandator;
using Woozle.Services.UserManagement;

namespace Woozle.Services.Authentication
{
    [MandatorAuthenticate]
    public class LoginContextService : MandatorAuthenticatedService
    {
        /// <summary>
        /// Gets the context of the user which is logged in to this Session (User and Mandator)
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [ExceptionCatcher]
        public LoginContextResult Get(LoginContext requestDto)
        {
            var serviceUser = Mapper.Map<Model.User, User>(Session.SessionData.User);
            var serviceMandator = Mapper.Map<Model.Mandator, Mandator.Mandator>(Session.SessionData.Mandator);
            var result = new LoginContextResult {User = serviceUser, Mandator = serviceMandator};
            return result;
        }
       
    }
}
