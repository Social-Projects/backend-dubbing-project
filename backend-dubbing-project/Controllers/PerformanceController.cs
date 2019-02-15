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
    [ApiController]
    [Route("api/performance")]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;

        public PerformanceController(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        /// <summary>
        /// Get all performances.
        /// </summary>
        /// <returns>Array of performances.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Performance>>> Get()
        {
            var listOfAllPerformances = await _performanceService.GetAllPerformances();

            var listIsEmpty = listOfAllPerformances.Count == 0;

            if (listIsEmpty)
                return NotFound();

            return Ok(listOfAllPerformances);
        }

        /// <summary>
        /// Get all the performance's speeches.
        /// </summary>
        /// <returns>Array of audio.</returns>
        /// <response code="200">Returns the array of audios of the performance with the following id.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpGet("{id}/speeches")]
        public async Task<ActionResult<ICollection<Speech>>> GetSpeeches(int id)
        {
            var listOfSpeeches = await _performanceService.GetSpeeches(id);

            var listIsEmpty = listOfSpeeches.Count == 0;

            if (listIsEmpty)
                return NotFound();

            return Ok(listOfSpeeches);
        }

        /// <summary>
        /// Get performance by id.
        /// </summary>
        /// <returns>Performance with the following id.</returns>
        /// <response code="200">Returns the performance with the following id.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetById(int id)
        {
            var performance = await _performanceService.GetPerformanceById(id);

            if (performance == null)
                return NotFound();

            return Ok(performance);
        }

        /// <summary>
        /// Creates a new performance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created performance.</returns>
        /// <response code="201">Returns the newly created performance.</response>
        /// <response code="400">If the performance is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<Performance>> Create(Performance model)
        {
            await _performanceService.CreatePerformance(model);

            return CreatedAtAction(nameof(GetById), new {id = model.Id}, model);
        }

        /// <summary>
        /// Updates the performance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An updated performance.</returns>
        /// <response code="200">Returns the updated performance.</response>
        /// <response code="400">If the performance is not valid.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpPut]
        public async Task<ActionResult<Performance>> Update(Performance model)
        {
            var performance = await _performanceService.UpdatePerformance(model);

            if (performance == null)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes the performance.
        /// </summary>
        /// <param name="id">Performance id.</param>
        /// <returns>Deleted performance.</returns>
        /// <response code="200">Returns the deleted performance.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Performance>> Delete(int id)
        {
            await _performanceService.DeletePerformance(id);

            return NoContent();
        }
    }
}