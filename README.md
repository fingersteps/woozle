#Woozle

Woozle is a .NET business application framework which will increase your development speed and helps you to achieve the return on investment faster. Woozle provides you the following features:

* [Multitenancy](https://github.com/fingersteps/woozle/wiki/Multitenancy)
* [Authentication](https://github.com/fingersteps/woozle/wiki/Authentication)
* [Modularity](https://github.com/fingersteps/woozle/wiki/Modularity)
* [Permission management](https://github.com/fingersteps/woozle/wiki/Permission-Management)
* [Multilanguage support](https://github.com/fingersteps/woozle/wiki/Multilanguage-support)
* [Integration of external systems](https://github.com/fingersteps/woozle/wiki/Integration-of-external-systems)
* [Model generator](https://github.com/fingersteps/woozle.generators)
* [Repository / Unit of Work generator](https://github.com/fingersteps/woozle.generators)


[![Build status](https://ci.appveyor.com/api/projects/status?id=b0hyo0w1s3movd6s)](https://ci.appveyor.com/project/woozles-woozle)

##Demo
Check out woozle in action here: http://woozle-demo.azurewebsites.net

The source code of the demo application is available here: https://github.com/woozles/woozle.examples

##Install
Woozle can be installed easily with [NuGet](http://nuget.org). To install it, run the following command in the Package Manager Console:

    PM> Install-Package Woozle

##Documentation
See [Woozle Wiki](https://github.com/fingersteps/woozle/wiki)

##Getting Started

###Step 1: Create the database
Create a new database for your application which contains all Woozle tables needed. To initialize all database related Woozle stuff, use the following scripts:

* [Create Woozle tables](https://github.com/fingersteps/woozle/blob/master/init/01_Create_Database.sql)
* [Create sample mandator](https://github.com/fingersteps/woozle/blob/master/init/02_Create_Mandator.sql)
* [Create sample user](https://github.com/fingersteps/woozle/blob/master/init/03_Create_User.sql)

###Step 2: Create an appliation
Create an empty ASP.NET web application in visual studio.

###Step 3: Install Woozle
Install Woozle (see instructions above) and add it to your created project.

###Step 4: Configure the database connection string and the web server
Replace the default configuration in your `Web.config` by the following configuration. The configuration includes a default connection string to the database (change it if necessary!) and the web server configuration to use Woozles built in web services.

```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
  For more information on how to configure your ASP.NET application, please visit
  http://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>
  <connectionStrings>
    <add name="EfWoozleEntity" connectionString="metadata=res://*/WoozleModel.csdl|res://*/WoozleModel.ssdl|res://*/WoozleModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;Integrated Security=SSPI;initial catalog=Woozle;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
  <system.web>
    <customErrors mode="Off" />
    <compilation debug="true" targetFramework="4.5" />
    <httpRuntime targetFramework="4.5" />
    <pages>
      <namespaces>
        <add namespace="System.Web.Routing" />
      </namespaces>
    </pages>
    <httpHandlers>
      <add path="api*" type="ServiceStack.WebHost.Endpoints.ServiceStackHttpHandlerFactory, ServiceStack" verb="*" />
    </httpHandlers>
  </system.web>
  <system.webServer>
    <defaultDocument enabled="true" />
    <validation validateIntegratedModeConfiguration="false" />
    <modules runAllManagedModulesForAllRequests="true" />
    <httpProtocol>
      <customHeaders>
        <add name="Access-Control-Allow-Origin" value="*" />
        <add name="Access-Control-Allow-Headers" value="Content-Type" />
      </customHeaders>
    </httpProtocol>
  </system.webServer>
  <location path="api">
    <system.web>
      <httpHandlers>
        <add path="*" type="ServiceStack.WebHost.Endpoints.ServiceStackHttpHandlerFactory, ServiceStack" verb="*" />
      </httpHandlers>
    </system.web>
    <!-- Required for IIS 7.0 -->
    <system.webServer>
      <modules runAllManagedModulesForAllRequests="true" />
      <validation validateIntegratedModeConfiguration="false" />
      <handlers>
        <add path="*" name="ServiceStack.Factory" type="ServiceStack.WebHost.Endpoints.ServiceStackHttpHandlerFactory, ServiceStack" verb="*" preCondition="integratedMode" resourceType="Unspecified" allowPathInfo="true" />
      </handlers>
    </system.webServer>
  </location>
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="v11.0" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
</configuration>
```

###Step 5: Register Woozle
Create a Global Application Class `Global.asax.cs` and add the following code to startup Woozle when your application gets started.


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

###Step 6: Start your Applicationa
Woozle is now integrated in your application! To see all built in web services, start your Application and open the following URL.

    http://localhost:yourport/api/metadata
    
Dive deep into Woozle using our [Wiki](https://github.com/fingersteps/woozle/wiki)!



