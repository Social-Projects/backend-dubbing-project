using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/audio")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AudioController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]Audio model)
        {
            // Path to '~/wwwroot'
            var path = _hostingEnvironment.WebRootPath;

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
