using System;
using System.Threading;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using Auth = ServiceStack.Common.ServiceClient.Web.Auth;

namespace Woozle.Demo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonClient = new JsonServiceClient("http://localhost:1337/");
            Thread.Sleep(5000);
            var result = jsonClient.Post(new Auth
            {
                provider = CredentialsAuthProvider.Name,
                UserName = "pro",
                Password = "test123",
                RememberMe = true, //important tell client to retain permanent cookies
            });

            Console.WriteLine("Username: {0}, SessionId: {1}", result.UserName, result.SessionId);

            Console.ReadLine();
        }
    }
}
