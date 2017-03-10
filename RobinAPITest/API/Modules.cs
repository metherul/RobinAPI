using Nancy;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using RobinAPITest.Json;
using RobinAPITest.POSTGRESQL;
using RobinAPITest.Tools;
using System;

namespace RobinAPITest.API
{
    internal class Modules
    {
        private static void Main(string[] args) => new Modules().Start();

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

    public class Module : NancyModule
    {
        public Module()
        {
            // http://localhost/api/users/getlocation/{USERNAME}
            // Returns the selected users location.
            Get["/api/users/getlocation/{username}"] = x =>
            {
                // Pull out the username from the parameters.
                var username = x.username;
                string location = null;

                // Make a query to the database
                // and read from the results.
                using (var dataReader = new Database().Query($"select (location) from users where name ilike '{username}'"))
                {
                    dataReader.Read();
                    location = dataReader.GetString(0);
                }

                // Create a new PlayerLocation object.
                PlayerLocation playerLocation = new PlayerLocation();
                playerLocation.Status = StatusCodes.OK200;
                playerLocation.Location = location;
                playerLocation.XCoordinant = new LocationConvert().GetX(location);
                playerLocation.YCoordinant = new LocationConvert().GetY(location);

                // Return the serialized JSON object.
                return Response.AsText(JsonConvert.SerializeObject(playerLocation, Formatting.Indented));
            };

            Get["/"] = _ =>
            {
                return Response.AsText("Hello!");
            };
        }
    }
}