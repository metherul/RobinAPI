using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobinAPI.Tools;
using Nancy;

namespace RobinAPI.JSON
{
    public class GenericResponse
    {
        public GenericResponse(HttpStatusCode statusCode)
        {
            string Status = statusCode.ToString();
        }
    }
}
