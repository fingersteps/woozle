using System;

namespace Woozle.Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://*:1337/" : args[0];
            var appHost = new IntegratedSystemHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("IntegratedSystemHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadKey();
        }
    }
}
