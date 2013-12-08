﻿using System;
using ServiceStack;
using ServiceStack.Web;
using Woozle.Core.Model.SessionHandling;

namespace Woozle.Core.Services.Stack.Impl.Authentication
{
    /// <summary>
    /// Indicates that the request dto, which is associated with this attribute,
    /// requires authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MandatorAuthenticateAttribute : AuthenticateAttribute
    {
        public MandatorAuthenticateAttribute() : base(ServiceStack.Auth.CredentialsAuthProvider.Name)
        {
            
        }

        /// <summary>
        /// <see cref="AuthenticateAttribute"/>
        /// </summary>
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            SessionFeature.AddSessionIdToRequestFilter(req, res, null); //Required to get req.GetSessionId()

            //Get current session
            var session = req.GetSession() as Session;

            if (session != null)
            {
                //Check the session object if the mandator is set.
                if (session.SessionObject != null && session.SessionObject.Mandator == null && session.SessionObject.User != null)
                {
                    //If not throw a specific HttpError
                    throw new HttpError(601, "Please select a mandator.");
                }

                if (session.SessionObject != null && session.SessionObject.User == null &&
                         session.SessionObject.Mandator == null)
                {
                    if (!session.IsAuthenticated)
                    {
                        throw new HttpError(401, "Unauthorized");
                    }
                }
            }

            base.Execute(req, res, requestDto);
        }
    }
}