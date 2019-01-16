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
    [Route("api/streaming")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private const int FAILSTATUSCODE = StatusCodes.Status406NotAcceptable;

        private IRepository<Performance> _perfRepo;

        private IStreamService _streamService;

        public StreamController(IStreamService streamService, IRepository<Performance> repository)
        {
            _perfRepo = repository;
            _streamService = streamService;
        }

        [HttpGet("load/{performanceId}")]
        public IActionResult LoadPerformance(int performanceId)
        {
            _streamService.Load(_perfRepo.GetItem(performanceId, "Speeches", "Speeches.Audios").Speeches);
            return Ok();
        }

        [HttpGet("currentAudio")]
        public ActionResult<string> GetCurrentAudio([FromQuery] int langId)
        {
            if (_streamService.IsPaused)
                return StatusCode(FAILSTATUSCODE);

            var audio = _streamService.CurrentSpeech.Audios.FirstOrDefault(x => x.LanguageId == langId);

            if (audio == null)
                return NotFound();

            return audio.FileName;
        }

        [HttpPost("nextSpeech")]
        public IActionResult PlayNext()
        {
            if (_streamService.PlayNext() == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
        }

        [HttpPost("prevSpeech")]
        public IActionResult PlayPrevious()
        {
            if (_streamService.PlayPrevious() == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
        }

        [HttpPost("pause")]
        public void Pause()
        {
            _streamService.Pause();
        }

        [HttpPost("play")]
        public void Play()
        {
            _streamService.Play();
        }

        [HttpPost("play/{index}")]
        public IActionResult Play(int index)
        {
            if (_streamService.Play(index) == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
        }

        [HttpGet("currentSpeechId")]
        public ActionResult<int> GetCurrentSpeechId()
        {
            return _streamService.Speeches.IndexOf(_streamService.CurrentSpeech);
        }
    }
}