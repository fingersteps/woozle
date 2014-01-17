#Woozle

Woozle is a .NET business application framework which will increase your development speed and helps you to achieve the return on investment faster. Woozle provides you the following features:

* Multitenancy
* Authentication
* Permission management
* Multilanguage support
* Integration of external systems
* Model generator
* Repository / Unit of Work generator
* Reporting (coming soon)
* RESTful web services (using ServiceStack)


[![Build status](https://ci.appveyor.com/api/projects/status?id=b0hyo0w1s3movd6s)](https://ci.appveyor.com/project/woozles-woozle)

##Demo
Check out woozle in action here: http://woozle-demo.azurewebsites.net

The source code of the demo application is available here: https://github.com/woozles/woozle.examples

##Install
Woozle can be installed easily with [NuGet](http://nuget.org). To install it, run the following command in the Package Manager Console:

    PM> Install-Package Woozle

##Getting started

###Step 1: Create the database
Create a new database for your application which contains all needed Woozle tables. To initialize all database related Woozle stuff, use the following scripts:

* [Create Woozle tables](https://github.com/fingersteps/woozle/blob/master/init/01_Create_Database.sql)
* [Create sample mandator](https://github.com/fingersteps/woozle/blob/master/init/02_Create_Mandator.sql)
* [Create sample user](https://github.com/fingersteps/woozle/blob/master/init/03_Create_User.sql)

###Step 1: Create an Appliation
Create an empty ASP.NET Web Application in Visual Studio.

###Step 2: Install Woozle
Install Woozle (see instructions above) and add it to your created project.

###Step 4: Configure Woozle
TODO

###Step 3: Register Woozle and start your application
Add the following code to your `Global.asax.cs` to startup Woozle when your application gets started.


```csharp
public class Global : System.Web.HttpApplication
{
    public class AppHost : WoozleHost
    {
        public AppHost() : base("Your Application", typeof (WoozleHost).Assembly) { }

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
```
