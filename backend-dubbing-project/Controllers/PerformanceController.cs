using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [ApiController]
    [Route("api/performance")]
    public class PerformanceController : ControllerBase
    {
        private readonly IRepository<Performance> _performances;

        public PerformanceController(IRepository<Performance> performances)
        {
            _performances = performances;
        }

        /// <summary>
        /// Get all performances.
        /// </summary>
        /// <returns>Array of performances.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Performance>>> Get()
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

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
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != id);

            if (doesNotExist)
                return NotFound();

            var performance = await _performances.GetItemAsync(
                id,
                source => source.Include(x => x.Speeches).ThenInclude(y => y.Audios));

            var speeches = performance.Speeches;

            return Ok(speeches);
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
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != id);

            if (doesNotExist)
                return NotFound();

            var performance = await _performances.GetItemAsync(id);

            return performance;
        }

        /// <summary>
        /// Creates a new performance.
        /// </summary>
        /// <param name="performance"></param>
        /// <returns>A newly created performance.</returns>
        /// <response code="201">Returns the newly created performance.</response>
        /// <response code="400">If the performance is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<Performance>> Create(Performance performance)
        {
            await _performances.CreateAsync(performance);

            return CreatedAtAction(nameof(GetById), new { id = performance.Id }, performance);
        }

        /// <summary>
        /// Updates the performance.
        /// </summary>
        /// <param name="performance"></param>
        /// <returns>An updated performance.</returns>
        /// <response code="200">Returns the updated performance.</response>
        /// <response code="400">If the performance is not valid.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpPut]
        public async Task<ActionResult<Performance>> Update(Performance performance)
        {
            var listOfAllPerformances = await _performances.GetAllItemsAsync();

            var doesNotExist = listOfAllPerformances.All(x => x.Id != performance.Id);

            if (doesNotExist)
                return NotFound();

            await _performances.UpdateAsync(performance);

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
            var list = await _performances
                .GetAllItemsAsync(source => source.Include(x => x.Speeches)
                    .ThenInclude(y => y.Audios));

            var performance = list.FirstOrDefault(x => x.Id == id);

            if (performance == null)
                return NotFound();

            foreach (var speech in performance.Speeches)
            {
                foreach (var audio in speech.Audios)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles\", audio.FileName);
                    try
                    {
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        else
                        {
                            Console.WriteLine($"File '{path}' not found!");
                        }
                    }
                    catch (IOException ioExc)
                    {
                        Console.WriteLine(ioExc);
                    }
                }
            }

            await _performances.DeleteAsync(performance);

            return NoContent();
        }
    }
}