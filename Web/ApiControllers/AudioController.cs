using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;

        public AudioController(IAudioService audioService, IMapper mapper, IFileRepository fileRepository)
        {
            _audioService = audioService;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        /// <summary>Controller method for getting a list of all audios.</summary>
        /// <returns>List of all audios.</returns>
        /// <response code="200">Is returned when the list has at least one audio.</response>
        /// <response code="404">Is returned when the list of audios is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<AudioDTO>>> GetAll()
        {
            var listOfAudios = await _audioService.GetAllAsync();

            var listOfAudiosDTOs = _mapper.Map<IEnumerable<Audio>, IEnumerable<AudioDTO>>(listOfAudios);

            return Ok(listOfAudiosDTOs);
        }

        /// <summary>Controller method for getting a audio by id.</summary>
        /// <param name="id">Id of audio that need to receive.</param>
        /// <returns>The audio with the following id.</returns>
        /// <response code="200">Is returned when audio does exist.</response>
        /// <response code="404">Is returned when audio with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AudioDTO>> GetById(int id)
        {
            var audio = await _audioService.GetByIdAsync(id);

            if (audio == null)
                return NotFound();

            var audioDTO = _mapper.Map<Audio, AudioDTO>(audio);

            return Ok(audioDTO);
        }

        /// <summary>Controller method for creating new audio.</summary>
        /// <param name="audioDTO">Audio model which needed to create.</param>
        /// <returns>Status code and audio.</returns>
        /// <response code="201">Is returned when audio is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when audio with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<AudioDTO>> Create(AudioDTO audioDTO)
        {
            var audio = _mapper.Map<AudioDTO, Audio>(audioDTO);

            await _audioService.CreateAsync(audio);
            audioDTO.Id = audio.Id;

            return CreatedAtAction(nameof(GetById), new {id = audioDTO.Id}, audioDTO);
        }

        /// <summary>Controller method for uploading a file to server and saving it to a local storage.</summary>
        /// <returns>Status code, URL of audio file and audio model.</returns>
        /// <param name="audioFileDTO">Audio file which needed to create.</param>
        /// <response code="201">Is returned when audio is successfully uploaded.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        [HttpPost("upload")]
        public async Task<ActionResult<AudioFileDTO>> Upload([FromForm] AudioFileDTO audioFileDTO)
        {
            var audio = _mapper.Map<AudioFileDTO, Audio>(audioFileDTO);

            using (var memStream = new MemoryStream())
            {
                audioFileDTO.File.CopyTo(memStream);

                audio.AudioFile = memStream.ToArray();

                audio.FileName = audioFileDTO.File.FileName;
            }

            await _audioService.UploadAsync(audio);

            var audioDTO = _mapper.Map<Audio, AudioFileDTO>(audio);

            var urlOfAudioFile = HttpContext.Request.Host.Value + "/audio/" + audio.FileName;

            return Created(urlOfAudioFile, audioDTO);
        }

        /// <summary>Controller method for updating an already existing audio with following id.</summary>
        /// <param name="id">Id of the audio that is needed to be updated.</param>
        /// <param name="audioDTO">The audio model to which is needed to be updated existing audio.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when speech is successfully updated.</response>
        /// <response code="400">Is returned when speech with or invalid data is passed.</response>
        /// <response code="404">Is returned when speech with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AudioDTO audioDTO)
        {
            if (audioDTO.Id != id)
                BadRequest();

            var audio = _mapper.Map<AudioDTO, Audio>(audioDTO);

            await _audioService.UpdateAsync(id, audio);

            return NoContent();
        }

        /// <summary>
        /// Unload audio from server
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpDelete("{fileName}")]
        public ActionResult Unload(string fileName)
        {
            string fullPath = Path.GetFullPath("./AudioFiles/");
            _fileRepository.Unload(fullPath + fileName);
            return NoContent();
        }
    }
}