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

                if (session != null && session.SessionData != null)
                {
                    if (session.SessionData.User == null || session.SessionData.Mandator == null)
                    {
                        throw new ArgumentException("SessionData, User and Mandator can not be null.");
                    }

                    //Check the session object if the mandator is set.
                    if (session.SessionData.Mandator.Id == 0 && session.SessionData.User.Id != 0)
                    {
                        //If not throw a specific HttpError
                        throw new HttpError(601, "Please select a mandator.");
                    }

                    if (session.SessionData.User.Id == 0 &&
                             session.SessionData.Mandator.Id == 0)
                    {
                        if (!session.IsAuthenticated)
                        {
                            throw new HttpError(401, "Unauthorized");
                        }
                    }
                }
                
                // Renew the timespan of the session.
                // http://teadriven.me.uk/2013/02/14/sliding-sessions-in-service-stack/
                req.SaveSession(session, new TimeSpan(0,0,30,0));

                base.Execute(req, res, requestDto);
        }
    }
}
