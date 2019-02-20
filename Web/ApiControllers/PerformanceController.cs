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
    /// <summary>
    /// API for managing operations with performances.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : Controller
    {
        private readonly IPerformanceService _performanceService;
        private readonly IMapper _mapper;

        public PerformanceController(IPerformanceService performanceService, IMapper mapper)
        {
            _performanceService = performanceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all performances.
        /// </summary>
        /// <returns>The list of all performances.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceViewModel>>> Get()
        {
            var performances = await _performanceService.GetAllAsync();
            var mappedPerformances = _mapper.Map<IEnumerable<Performance>, IEnumerable<PerformanceViewModel>>(performances);

            return mappedPerformances.ToList();
        }

        /// <summary>
        /// Get performance by id.
        /// </summary>
        /// <param name="id">Performance id.</param>
        /// <returns>The performance with the following id.</returns>
        /// <response code="200">If the performance with following id was founded.</response>
        /// <response code="404">If the performance is not existed with specified id.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceViewModel>> GetById(int id)
        {
            var performance = await _performanceService.GetByIdAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            var mappedPerformance = _mapper.Map<Performance, PerformanceViewModel>(performance);

            return mappedPerformance;
        }

        /// <summary>
        /// Get speeches of the performance by id.
        /// </summary>
        /// <param name="id">Performance id, speeches of that is needed to be returned.</param>
        /// <returns>The speeches of the performance with following id.</returns>
        /// <response code="200">If the performance was founded with following id.</response>
        /// <response code="404">If the performance is not existed with following id.</response>
        [HttpGet("{id}/speeches")]
        public async Task<ActionResult<IEnumerable<SpeechViewModel>>> GetSpeeches(int id)
        {
            var speeches = await _performanceService.GetSpeechesAsync(id);

            if (speeches == null)
            {
                return NotFound();
            }

            var mappedSpeeches = _mapper.Map<IEnumerable<Speech>, IEnumerable<SpeechViewModel>>(speeches);

            return mappedSpeeches.ToList();
        }

        /// <summary>
        /// Create performance.
        /// </summary>
        /// <param name="performance">The performance that is needed to be created.</param>
        /// <returns>The created performance.</returns>
        /// <response code="201">If the performance was created successfully.</response>
        /// <response code="400">If the passed performance is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<PerformanceViewModel>> Create(PerformanceViewModel performance)
        {
            if (ModelState.IsValid)
            {
                var originPerformance = _mapper.Map<PerformanceViewModel, Performance>(performance);
                await _performanceService.CreateAsync(originPerformance);

                var mappedPerformance = _mapper.Map<Performance, PerformanceViewModel>(originPerformance);

                return CreatedAtAction(nameof(GetById), new { id = mappedPerformance.Id }, mappedPerformance);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update performance.
        /// </summary>
        /// <param name="performance">The performance that is needed to be updated.</param>
        /// <param name="id">The performance id that is needed to be updated.</param>
        /// <returns>The updated performance.</returns>
        /// <response code="200">If the performance has been updated successfully.</response>
        /// <response code="400">If the passed performance is not valid.</response>
        /// <response code="404">If the passed performance has not been founded.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<PerformanceViewModel>> Update(PerformanceViewModel performance, int id)
        {
            if (ModelState.IsValid && performance.Id == id)
            {
                var originPerformance = _mapper.Map<PerformanceViewModel, Performance>(performance);

                var updatedPerformance = await _performanceService.UpdateAsync(originPerformance);

                if (updatedPerformance == null)
                {
                    return NotFound();
                }
                else
                {
                    var mappedPerformance = _mapper.Map<Performance, PerformanceViewModel>(updatedPerformance);
                    return mappedPerformance;
                }
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete performance by id.
        /// </summary>
        /// <param name="id">The performance id.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the performance was deleted successfully.</response>
        /// <response code="404">If the performance with following id is not existed.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedPerformance = await _performanceService.DeleteAsync(id);

            if (deletedPerformance == null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
