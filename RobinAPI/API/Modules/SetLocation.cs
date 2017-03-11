using Nancy;
using Newtonsoft.Json;
using RobinAPI.JSON;
using RobinAPI.POSTGRESQL;
using System;

namespace RobinAPI.API.Modules
{
    public class SetLocation : NancyModule
    {
        public SetLocation()
        {
            // Sets the current location of the user in the database.
            // /api/users/setlocation/&username={USERNAME}&x={XCOORDINANT}&y={YCOORDINANT}
            Post["/api/users/setlocation/{parameters}"] = x =>
            {
                var parameters = x.parameters.ToString().Split('&');
                var username = parameters[1].Replace("username=", "");
                var xCoordinant = parameters[2].Replace("x=", "");
                var yCoordinant = parameters[3].Replace("y=", "");
                var requestText = $"update users set location='({xCoordinant}, {yCoordinant})' where name='{username}'";
                var ip = Request.UserHostAddress;

                GenericResponse response;

                Console.WriteLine($"/api/users/setlocation/{x.parameters}");
                Console.WriteLine($"RESULT CAPTURED - USERNAME: {username}, IP: {ip}, TIME: {DateTime.UtcNow} \n");

                using (var dataReader = new Database().Query(requestText))
                {
                    response = new GenericResponse(HttpStatusCode.OK);
                }

                var jsonResponse = (Response)JsonConvert.SerializeObject(response, Formatting.Indented);
                jsonResponse.ContentType = "application/json";

                return jsonResponse;
            };
        }
    }
}