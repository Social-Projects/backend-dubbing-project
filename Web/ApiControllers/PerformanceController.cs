using System.Collections.Generic;
using System.Linq;
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
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;
        private readonly IMapper _mapper;

        public PerformanceController(IPerformanceService performanceService, IMapper mapper)
        {
            _performanceService = performanceService;
            _mapper = mapper;
        }

        /// <summary>Controller method for getting a list of all performances.</summary>
        /// <returns>List of all performances.</returns>
        /// <response code="200">Is returned when the list has at least one performance.</response>
        /// <response code="404">Is returned when the list of performances is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<PerformanceDTO>>> GetAll()
        {
            var listOfPerformances = await _performanceService.GetAllAsync();

            var listOfPerformanceDTOs = _mapper.Map<List<Performance>, List<PerformanceDTO>>(listOfPerformances);

            return Ok(listOfPerformanceDTOs);
        }

        /// <summary>Controller method for getting a performance by id.</summary>
        /// <param name="id">Id of performance that need to receive.</param>
        /// <returns>The performance with the following id.</returns>
        /// <response code="200">Is returned when performance does exist.</response>
        /// <response code="404">Is returned when performance with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceDTO>> GetById(int id)
        {
            var performance = await _performanceService.GetByIdAsync(id);

            if (performance == null)
                return NotFound();

            var performanceDTO = _mapper.Map<Performance, PerformanceDTO>(performance);

            return Ok(performanceDTO);
        }

        /// <summary>Controller method for getting a speeches by id of performance.</summary>
        /// <param name="id">Id of performance which speeches that need to receive.</param>
        /// <returns>List of a speeches.</returns>
        /// <response code="200">Is returned when speeches does exist.</response>
        /// <response code="400">Is returned when performance with such Id doesn't exist.</response>
        /// <response code="404">Is returned when speeches doesn't exist.</response>
        [HttpGet("{id}/speeches")]
        public async Task<ActionResult<List<SpeechDTO>>> GetByIdWithChildren(int id)
        {
            var listOfSpeeches = await _performanceService.GetChildrenByIdAsync(id);

            if (listOfSpeeches == null)
                return BadRequest($"Performance with Id: {id} doesn't exist!");

            if (!listOfSpeeches.Any())
                return NotFound();

            var listOfSpeechDTOs = _mapper.Map<List<Speech>, List<SpeechDTO>>(listOfSpeeches);

            return Ok(listOfSpeechDTOs);
        }

        /// <summary>Controller method for creating new performance.</summary>
        /// <param name="performanceDTO">Performance model which needed to create.</param>
        /// <returns>Status code and performance.</returns>
        /// <response code="201">Is returned when performance is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when performance with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<PerformanceDTO>> Create(PerformanceDTO performanceDTO)
        {
            var performance = _mapper.Map<PerformanceDTO, Performance>(performanceDTO);

            await _performanceService.CreateAsync(performance);

            return CreatedAtAction(nameof(GetById), new {id = performanceDTO.Id}, performanceDTO);
        }

        /// <summary>Controller method for updating an already existing performance with following id.</summary>
        /// <param name="id">Id of the performance that is needed to be updated.</param>
        /// <param name="performanceDTO">The performance model that is needed to be created.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when performance is successfully updated.</response>
        /// <response code="400">Is returned when performance with or invalid data is passed.</response>
        /// <response code="404">Is returned when performance with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PerformanceDTO performanceDTO)
        {
            if (performanceDTO.Id != id)
                BadRequest();

            var performance = _mapper.Map<PerformanceDTO, Performance>(performanceDTO);

            await _performanceService.UpdateAsync(id, performance);

            return NoContent();
        }

        /// <summary>Controller method for deleting an already existing performance with following id.</summary>
        /// <param name="id">Id of the performance that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when performance is successfully deleted.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _performanceService.DeleteAsync(id);

            return NoContent();
        }
    }
}