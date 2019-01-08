using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_dubbing_project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace backend_dubbing_project.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class AudioUploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AudioUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("api/upload")]
        public async Task<IActionResult> Upload(Translation model)
        {
            if (model == null)
                throw new ArgumentNullException();

            if (model.AudioFile == null || model.AudioFile.Length <= 0)
                throw new Exception("Audio file is empty");

            // Path to '~/wwwroot'
            string path = _hostingEnvironment.WebRootPath;


            // Example of saving file to local root '~/wwwroot'
            using (var fileStream = new FileStream(Path.Combine(path, model.AudioFile.FileName), FileMode.Create))
            {
                await model.AudioFile.CopyToAsync(fileStream);
            }

            // Unit of work expected here
            //
            // unitOfWork.AudioUpload.Create(model);
            // await unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
