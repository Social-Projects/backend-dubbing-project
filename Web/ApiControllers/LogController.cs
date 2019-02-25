using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string _fileInfoPath;
        private readonly string _fileErrorPath;

        public LogController(IConfiguration configuration)
        {
            string folderPath = configuration["Logging:Path:Folder"];

            _fileInfoPath = Path.Combine(folderPath, configuration["Logging:Path:Informations"]);
            _fileErrorPath = Path.Combine(folderPath, configuration["Logging:Path:Errors"]);
        }

        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<LoggingObject>>> GetInfo()
        {
            if (System.IO.File.Exists(_fileInfoPath))
            {
                var objects = await System.IO.File.ReadAllTextAsync(_fileInfoPath);
                var encodedObjects = JsonConvert.DeserializeObject<IEnumerable<LoggingObject>>(objects);

                return encodedObjects.ToList();
            }

            return NotFound(new { Message = "File with informations is not existed!" });
        }

        [HttpGet("errors")]
        public async Task<ActionResult<IEnumerable<LoggingObject>>> GetError()
        {
            if (System.IO.File.Exists(_fileErrorPath))
            {
                var objects = await System.IO.File.ReadAllTextAsync(_fileErrorPath);
                var encodedObjects = JsonConvert.DeserializeObject<IEnumerable<LoggingObject>>(objects);

                return encodedObjects.ToList();
            }

            return NotFound(new { Message = "File with errors is not existed!" });
        }
    }
}