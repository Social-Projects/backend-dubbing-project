using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using Web.ViewModels;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/audio")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;
        private readonly IMapper _mapper;

        public AudioController(IAudioService audioService, IMapper mapper)
        {
            _audioService = audioService;
            _mapper = mapper;
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

            var mappedAudios = _mapper.Map<IEnumerable<Audio>, IEnumerable<AudioFileViewModel>>(audios);

            return Ok(mappedAudios);
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

            var mappedAudio = _mapper.Map<Audio, AudioFileViewModel>(audio);

            return Ok(mappedAudio);
        }

        /// <summary>
        /// Creates a new audio
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created audio</returns>>
        /// <response code="204"></response>
        [HttpPost]
        public async Task<ActionResult> Create(AudioViewModel model)
        {
            Audio audio = _mapper.Map<AudioViewModel, Audio>(model);

            await _audioService.AddAsync(audio);

            return NoContent();
        }

        /// <summary>
        /// Uploads a file and saves it to a local storage
        /// </summary>
        /// <param name="model"></param>
        /// <response code="201"></response>
        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromForm] AudioFileViewModel model)
        {
            Audio audio = _mapper.Map<AudioFileViewModel, Audio>(model);

            using (var memStream = new MemoryStream())
            {
                model.File.CopyTo(memStream);

                audio.AudioFile = memStream.ToArray();

                audio.FileName = model.File.FileName;
            }

            await _audioService.UploadAsync(audio);

            var mappedAudio = _mapper.Map<Audio, AudioFileViewModel>(audio);

            return Ok(mappedAudio);
        }

        /// <summary>
        /// Update an audio
        /// </summary>
        /// <param name="model"></param>
        /// <response code="204"></response>
        /// <response code="400"></response>
        [HttpPut]
        public async Task<ActionResult> Update(AudioViewModel model)
        {
            Audio audio = _mapper.Map<AudioViewModel, Audio>(model);

            await _audioService.UpdateAsync(audio);

            // And we should return 200, or change logic on frontend

            return NoContent();
        }
    }
}