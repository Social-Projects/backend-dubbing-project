using System.Collections.Generic;
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
    public class SpeechController : ControllerBase
    {
        private readonly ISpeechService _speechService;
        private readonly IMapper _mapper;

        public SpeechController(ISpeechService speechService, IMapper mapper)
        {
            _speechService = speechService;
            _mapper = mapper;
        }

        /// <summary>Controller method for getting a list of all speeches.</summary>
        /// <returns>List of all speeches.</returns>
        /// <response code="200">Is returned when the list has at least one speech.</response>
        /// <response code="404">Is returned when the list of speeches is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<SpeechDTO>>> GetAll()
        {
            var listOfSpeeches = await _speechService.GetAllAsync();

            var listOfSpeechesDTOs = _mapper.Map<List<Speech>, List<SpeechDTO>>(listOfSpeeches);

            return Ok(listOfSpeechesDTOs);
        }

        /// <summary>Controller method for getting a speech by id.</summary>
        /// <param name="id">Id of speech that need to receive.</param>
        /// <returns>The speech with the following id.</returns>
        /// <response code="200">Is returned when speech does exist.</response>
        /// <response code="404">Is returned when speech with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<SpeechDTO>> GetById(int id)
        {
            var speech = await _speechService.GetByIdAsync(id);

            if (speech == null)
                return NotFound();

            var speechDTO = _mapper.Map<Speech, SpeechDTO>(speech);

            return Ok(speechDTO);
        }

        /// <summary>Controller method for creating new speech.</summary>
        /// <param name="speechDTO">Speech model which needed to create.</param>
        /// <returns>Status code and speech.</returns>
        /// <response code="201">Is returned when speech is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when speech with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<SpeechDTO>> Create(SpeechDTO speechDTO)
        {
            var speech = _mapper.Map<SpeechDTO, Speech>(speechDTO);

            await _speechService.CreateAsync(speech);

            return CreatedAtAction(nameof(GetById), new {id = speechDTO.Id}, speechDTO);
        }

        /// <summary>Controller method for updating an already existing speech with following id.</summary>
        /// <param name="id">Id of the speech that is needed to be updated.</param>
        /// <param name="speechDTO">The speech model that is needed to be updated.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when speech is successfully updated.</response>
        /// <response code="400">Is returned when speech with or invalid data is passed.</response>
        /// <response code="404">Is returned when speech with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SpeechDTO speechDTO)
        {
            if (speechDTO.Id != id)
                BadRequest();

            var speech = _mapper.Map<SpeechDTO, Speech>(speechDTO);

            await _speechService.UpdateAsync(id, speech);

            return NoContent();
        }

        /// <summary>Controller method for deleting an already existing speech with following id.</summary>
        /// <param name="id">Id of the speech that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when speech is successfully deleted.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _speechService.DeleteAsync(id);

            return NoContent();
        }
    }
}