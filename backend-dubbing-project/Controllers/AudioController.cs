using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Services;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/audio")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        /// <summary>
        /// Uploads a file and saves it to a local storage.
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] AudioDTO model)
        {
            await _audioService.Upload(model);

            return Ok();
        }

        /// <summary>
        /// Get all audios.
        /// </summary>
        /// <returns>Array of audios.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Audio>>> Get()
        {
            var listOfAllAudios = await _audioService.GetAllAudios();

            var listIsEmpty = listOfAllAudios.Count == 0;

            if (listIsEmpty)
                return NotFound();

            return Ok(listOfAllAudios);
        }

        /// <summary>
        /// Get audios by id.
        /// </summary>
        /// <returns>Audio with the following id.</returns>
        /// <response code="200">Returns the audio with the following id.</response>
        /// <response code="404">If the audio with the following id does not exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetById(int id)
        {
            var audio = await _audioService.GetAudioById(id);

            var audioIsNotExist = audio == null;

            if (audioIsNotExist)
                return NotFound();

            return Ok(audio);
        }

        /// <summary>
        /// Creates a new audio.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created audio.</returns>
        /// <response code="201">Returns the newly created audio.</response>
        /// <response code="400">If the audio is not valid.</response>
        [HttpPost]
        public async Task<ActionResult> Create(Audio model)
        {
            await _audioService.CreateAudio(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        /// <summary>
        /// Updates the audio.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An updated audio.</returns>
        /// <response code="200">Returns the updated audio.</response>
        /// <response code="400">If the audio is not valid.</response>
        /// <response code="404">If the audio with the following id does not exist.</response>
        [HttpPut]
        public async Task<ActionResult> Update(Audio model)
        {
            await _audioService.UpdateAudio(model);

            return NoContent();
        }
    }
}