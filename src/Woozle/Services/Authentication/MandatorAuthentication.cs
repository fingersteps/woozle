using System;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using Woozle.Model.SessionHandling;

namespace Woozle.Services.Authentication
{
    /// <summary>
    /// Indicates that the request dto, which is associated with this attribute,
    /// requires authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MandatorAuthenticateAttribute : AuthenticateAttribute
    {
        /// <summary>
        /// Initialize a new <see cref="MandatorAuthenticateAttribute"/>
        /// </summary>
        public MandatorAuthenticateAttribute()
            : base(AuthService.CredentialsProvider)
        {
            
        }

        public override void Execute(ServiceStack.ServiceHost.IHttpRequest req, ServiceStack.ServiceHost.IHttpResponse res, object requestDto)
        {
                SessionFeature.AddSessionIdToRequestFilter(req, res, null); //Required to get req.GetSessionId()

                //Get current session
                var session = req.GetSession() as Session;

                if (session != null)
                {
                    //Check the session object if the mandator is set.
                    if (session.SessionData != null && session.SessionData.Mandator == null && session.SessionData.User != null)
                    {
                        //If not throw a specific HttpError
                        throw new HttpError(601, "Please select a mandator.");
                    }

                    if (session.SessionData != null && session.SessionData.User == null &&
                             session.SessionData.Mandator == null)
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
