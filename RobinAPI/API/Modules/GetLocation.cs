using Nancy;
using Newtonsoft.Json;
using RobinAPI.JSON;
using RobinAPI.POSTGRESQL;
using RobinAPI.Tools;
using System;

namespace RobinAPI.API.Modules
{
    public class GetLocation : NancyModule
    {
        // Returns the selected users location.
        // http://localhost/api/users/getlocation/&username={USERNAME}
        public GetLocation()
        {
            Get["/api/users/getlocation/{parameters}"] = x =>
            {
                // Pull out the username from the parameters.
                var username = x.parameters.ToString().Replace("&username=", "");
                var ip = Request.UserHostAddress.ToString();

                string location;
                PlayerLocation playerLocation;

                // Just for testing.
                Console.WriteLine($"/api/users/getlocation/{username}");
                Console.WriteLine($"RESULT CAPTURED - USERNAME: {username}, IP: {ip}, TIME: {DateTime.UtcNow} \n");

                // Make a query to the database
                // and read from the results.
                using (var dataReader = new Database().Query($"select (location) from users where name='{username}'"))
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        location = dataReader.GetString(0);

                        // Playerlocation object if data is returned from the server.
                        playerLocation = new PlayerLocation();
                        playerLocation.Status = HttpStatusCode.OK.ToString();
                        playerLocation.Username = username;
                        playerLocation.Location = location;
                        playerLocation.XCoordinant = new LocationConvert().GetX(location);
                        playerLocation.YCoordinant = new LocationConvert().GetY(location);
                    }
                    else
                    {
                        // Playerlocation object if nothing is returned.
                        playerLocation = new PlayerLocation();
                        playerLocation.Status = HttpStatusCode.NotFound.ToString();
                        playerLocation.Username = username;
                        playerLocation.Location = "";
                        playerLocation.XCoordinant = "";
                        playerLocation.YCoordinant = "";
                    }
                }

                // Return the serialized JSON object.
                var jsonResponse = (Response)JsonConvert.SerializeObject(playerLocation, Formatting.Indented);
                jsonResponse.ContentType = "application/json";

                return jsonResponse;
            };
        }
    }
}