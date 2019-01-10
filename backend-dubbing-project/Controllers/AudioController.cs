using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dubbing.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace Dubbing.Controllers
{
    [ApiController]
    public class AudioController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AudioController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(Audio model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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
