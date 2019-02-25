using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoggingObject>>> Get()
        {
            var objects = await System.IO.File.ReadAllLinesAsync("log.json");
            var encodedObjects = new List<LoggingObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                encodedObjects.Add(JsonConvert.DeserializeObject<LoggingObject>(objects[i]));
            }

            return encodedObjects;
        }
    }
}