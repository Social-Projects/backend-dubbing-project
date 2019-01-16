using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Services;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/stream")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private IHostingEnvironment _environment;

        private IRepository<Performance> _perfRepo;

        private IStreamService _streamService;

        private bool _isPaused = true;

        public StreamController(IStreamService streamService, IRepository<Performance> repository, IHostingEnvironment environment)
        {
            _perfRepo = repository;
            _streamService = streamService;
            _environment = environment;
        }

        [HttpGet("load/{performanceId}")]
        public IActionResult LoadPerformance(int performanceId)
        {
            _streamService.Load(_perfRepo.GetItem(performanceId, "Speeches", "Speeches.Audios").Speeches);
            return Ok();
        }

        [HttpGet("currentAudio")]
        public ActionResult<FileResult> GetCurrentAudio([FromQuery] int langId)
        {
            var audio = _streamService.CurrentSpeech.Audios.FirstOrDefault(x => x.LanguageId == langId);

            if (audio == null)
                return NotFound();

            var path = _environment.WebRootPath;

            using (var fileStream = new FileStream(Path.Combine(path, audio.FileName), FileMode.Open))
            {
                return new FileStreamResult(fileStream, "audio/mp3");
            }
        }

        [HttpGet("currentFile")]
        public ActionResult<string> GetCurrentFile([FromQuery] int langId)
        {
            var audio = _streamService.CurrentSpeech.Audios.FirstOrDefault(x => x.LanguageId == langId);

            if (audio == null)
                return NotFound();

            var path = _environment.WebRootPath;

            return Path.Combine(path, audio.FileName);
        }

        [HttpGet("playNext")]
        public IActionResult PlayNext()
        {
            if (_streamService.PlayNext() == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("pause")]
        public void Pause()
        {
            _streamService.Pause();
        }

        [HttpGet("play")]
        public void Play()
        {
            _streamService.Play();
        }
    }
}