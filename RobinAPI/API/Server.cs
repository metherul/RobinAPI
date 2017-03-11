using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinAPI.API
{
    class Server
    {
        private static void Main(string[] args) => new Server().Start();

        private void Start()
        {
            var port = 5000;
            var path = "http://localhost";

            var uri = new Uri($"{path}:{port}");
            var host = new NancyHost(uri);

            // Start the server
            host.Start();

            Console.WriteLine($"Listening on port: {port}");

            // Just let it sit here.
            Console.ReadLine();

            // Then stop the server.
            host.Stop();
        }
    }
}
