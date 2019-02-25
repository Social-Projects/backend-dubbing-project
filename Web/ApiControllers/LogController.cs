using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly string _path;

        public LogController(IConfiguration configuration)
        {
            _path = configuration["Logging:path"];
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoggingObject>>> Get()
        {
            if (System.IO.File.Exists(_path))
            {
                var objects = await System.IO.File.ReadAllTextAsync(_path);
                var encodedObjects = JsonConvert.DeserializeObject<IEnumerable<LoggingObject>>(objects);

                return encodedObjects.ToList();
            }

            return NotFound("File is not existed!");
        }
    }
}