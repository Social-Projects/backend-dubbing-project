using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Services;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/streaming")]
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
            _streamService.Load(_perfRepo.GetItem(performanceId, source => source.Include(x => x.Speeches).ThenInclude(y => y.Audios)).Speeches);
            return Ok();
        }

        [HttpGet("currentAudio")]
        [ProducesResponseType(FAILSTATUSCODE)]
        [ProducesResponseType(404)]
        public ActionResult<string> GetCurrentAudio([FromQuery] int langId)
        {
            if (_streamService.IsPaused)
                return StatusCode(FAILSTATUSCODE);

            var audio = _streamService.CurrentSpeech.Audios.FirstOrDefault(x => x.LanguageId == langId);

            if (audio == null)
                return NotFound();

            return audio.FileName;
        }

        [HttpGet("nextSpeech")]
        [ProducesResponseType(FAILSTATUSCODE)]
        public IActionResult PlayNext()
        {
            if (_streamService.PlayNext() == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
        }

        [HttpGet("prevSpeech")]
        [ProducesResponseType(FAILSTATUSCODE)]
        public IActionResult PlayPrevious()
        {
            if (_streamService.PlayPrevious() == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
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

        [HttpGet("play/{index}")]
        [ProducesResponseType(FAILSTATUSCODE)]
        public IActionResult Play(int index)
        {
            if (_streamService.Play(index) == true)
                return Ok();
            else
                return StatusCode(FAILSTATUSCODE);
        }

        [HttpGet("currentSpeechId")]
        [ProducesResponseType(FAILSTATUSCODE)]
        public ActionResult<int> GetCurrentSpeechId()
        {
            if (_streamService.IsPaused == false)
                return _streamService.CurrentSpeech.Id;
            else
                return StatusCode(FAILSTATUSCODE);
        }
    }
}