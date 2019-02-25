using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using Web.ViewModels;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeechController : Controller
    {
        private readonly ISpeechService _speechService;
        private readonly IMapper _mapper;

        public SpeechController(ISpeechService speechService, IMapper mapper)
        {
            _speechService = speechService;
            _mapper = mapper;
        }

        /// <summary>
        /// Controller method for getting a list of all speeches.
        /// </summary>
        /// <returns>List of all speeches.</returns>
        /// <response code="200">Is returned when the list has at least one speech.</response>
        /// <response code="404">Is returned when the list of speeches is empty.</response>
        [HttpGet("all")]
        public async Task<ActionResult<List<Speech>>> GetAll()
        {
            var listOfAllSpeeches = await _speechService.GetAll();

            if (!listOfAllSpeeches.Any())
                return NotFound();

            return Ok(listOfAllSpeeches);
        }

//        /// <summary>
//        /// Controller method for getting a list of all speeches.
//        /// </summary>
//        /// <returns>List of all speeches.</returns>
//        /// <response code="200">Is returned when the list has at least one speech.</response>
//        /// <response code="404">Is returned when the list of speeches is empty.</response>
//        [HttpGet("all/performance/{id}")]
//        public async Task<ActionResult<List<Speech>>> GetAllByPerformanceId(int id)
//        {
//            var listOfAllSpeeches = await _speechService.GetAllSpeechesByPerformanceId(id);
//
//            if (!listOfAllSpeeches.Any())
//                return NotFound();
//
//            return Ok(listOfAllSpeeches);
//        }

        /// <summary>
        /// Controller method for getting a speech by id.
        /// </summary>
        /// <param name="id">Id of speech that need to receive.</param>
        /// <returns>Speech.</returns>
        /// <response code="200">Is returned when speech does exist.</response>
        /// <response code="404">Is returned when speech with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Speech>> GetById(int id)
        {
            var speech = await _speechService.GetById(id);

            if (speech == null)
                return NotFound();

            return Ok(speech);
        }

        /// <summary>
        /// Controller method for creating new speech.
        /// </summary>
        /// <param name="receivedSpeech">Speech model which needed to create.</param>
        /// <returns>Status code and speech.</returns>
        /// <response code="201">Is returned when speech is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when speech with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<Speech>> CreateSpeech(SpeechViewModel receivedSpeech)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var speech = _mapper.Map<SpeechViewModel, Speech>(receivedSpeech);

            await _speechService.Create(speech);

            return CreatedAtAction(nameof(GetById), new {id = receivedSpeech.Id}, receivedSpeech);
        }

        /// <summary>
        /// Controller method for updating an already existing speech with following id.
        /// </summary>
        /// <param name="id">Id of the speech that is needed to be updated.</param>
        /// <param name="receivedSpeech">The speech model that is needed to be created.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when speech is successfully updated.</response>
        /// <response code="400">Is returned when speech with such Id is not found or invalid data is passed.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSpeech(int id, SpeechViewModel receivedSpeech)
        {
            if (id != receivedSpeech.Id || !ModelState.IsValid)
                return BadRequest();

            var speech = _mapper.Map<SpeechViewModel, Speech>(receivedSpeech);

            await _speechService.Update(id, speech);

            return NoContent();
        }

        /// <summary>
        /// Controller method for deleting an already existing speech with following id.
        /// </summary>
        /// <param name="id">Id of the speech that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when speech is successfully updated.</response>
        /// <response code="400">Is returned when speech id is not valid</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpeech(int id)
        {
            await _speechService.Delete(id);

            return NoContent();
        }
    }
}