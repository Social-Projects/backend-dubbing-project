using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
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
        /// Get all audios
        /// </summary>
        /// <returns>List of audios</returns>>
        /// <response code="200">Returns the list of audios</response>
        [HttpGet]
        public async Task<ActionResult<List<Audio>>> GetAll()
        {
            IEnumerable<Audio> audios = await _audioService.ListAllAsync();

            // mapping audios

            return Ok(audios);
        }

        /// <summary>
        /// Get an audio by id
        /// </summary>
        /// <param name="id">Id of audio</param>
        /// <returns>Audio with the following id</returns>
        /// <response code="200">Returns the audio with the following id</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetById(int id)
        {
            Audio audio = await _audioService.GetById(id);

            if (audio == null)
                return NotFound();

            // mapping audio

            return Ok(audio);
        }

        /// <summary>
        /// Creates a new audio
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created audio</returns>>
        /// <response code="204"></response>
        [HttpPost]
        public async Task<ActionResult> Create(Audio model)
        {
            // Write audio from IFormFile to byte array

            await _audioService.AddAsync(model);

            // Mapping object

            return NoContent();
        }

        /// <summary>
        /// Uploads a file and saves it to a local storage
        /// </summary>
        /// <param name="model"></param>
        /// <response code="201"></response>
        [HttpPost]
        public async Task<ActionResult> Upload([FromForm] Audio model)
        {
            // Write audio from IFormFile to byte array

            await _audioService.UploadAsync(model);

            // Mapping object

            return Ok();
        }

        /// <summary></summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="204"></response>
        /// <response code="400"></response>
        [HttpPut]
        public async Task<ActionResult> Update(Audio model)
        {
            await _audioService.UpdateAsync(model);

            // Mapping object

            // And we should return 200, or change logic on frontend

            return NoContent();
        }
    }
}