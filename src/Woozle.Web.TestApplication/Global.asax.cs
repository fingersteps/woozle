using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.WebHost.Endpoints;
using Woozle.Host;

namespace Woozle.Web.TestApplication
{
    public class Global : System.Web.HttpApplication
    {
        public class AppHost : WoozleHost
        {
            public AppHost() : base("Your Application", typeof(WoozleHost).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                base.Configure(container);
                EndpointHostConfig.Instance.DefaultRedirectPath = "index.html";

                SetConfig(new EndpointHostConfig
                {
                    ServiceStackHandlerFactoryPath = "api",
                    MetadataRedirectPath = "api/metadata",
                    GlobalResponseHeaders =
                {
                    {"Access-Control-Allow-Origin", "*"},
                    {"Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS"},
                },
                });
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }
    }
}