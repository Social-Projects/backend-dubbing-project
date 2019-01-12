using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Dubbing.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Dubbing.Util;

namespace Dubbing.Controllers
{
    [ApiController]
    [Route("api/performance")]
    public class PerformanceController : ControllerBase
    {
        private IRepository<Performance> performances;
        public PerformanceController(IRepository<Performance> performances)
        {
            this.performances = performances;
        }
        /// <summary>
        /// Get all performances
        /// </summary>
        /// <returns>Array of performances</returns>
        [HttpGet]
        public IEnumerable<Performance> Get()
        {
            return performances.GetAllItems();
        }
        /// <summary>
        /// Get performance by id
        /// </summary>
        /// <returns>Performance with the following id</returns>
        /// <response code="200">Returns the performance with the following id</response>
        /// <response code="404">If the performance with the following id does not exist</response>        
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Performance> GetById(int id)
        {
            if (!performances.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return performances.GetItem(id);
        }
        /// <summary>
        /// Creates a new performance
        /// </summary>
        /// <param name="performance"></param>
        /// <returns>A newly created performance</returns>
        /// <response code="201">Returns the newly created performance</response>
        /// <response code="400">If the performance is not valid</response>          
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Performance> Create(Performance performance)
        {
            performances.Create(performance);
            return CreatedAtAction(nameof(GetById), new { id = performance.Id }, performance);
        }
        /// <summary>
        /// Updates the performance
        /// </summary>
        /// <param name="performance"></param>
        /// <returns>An updated performance</returns>
        /// <response code="200">Returns the updated performance</response>
        /// <response code="400">If the performance is not valid</response>
        /// <response code="404">If the performance with the following id does not exist</response>          
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Performance> Update(Performance performance)
        {
            if (!performances.GetAllItems().Any(x => x.Id == performance.Id))
                return NotFound();
            
            performances.Update(performance);
            return performance;
        }
        /// <summary>
        /// Deletes the performance
        /// </summary>
        /// <param name="id">Performance id</param>
        /// <returns>Deleted performance</returns>
        /// <response code="200">Returns the deleted performance</response>
        /// <response code="404">If the performance with the following id does not exist</response>     
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Performance> Delete(int id)
        {
            var list = performances.GetAllItems();
            var performance = list.FirstOrDefault(x => x.Id == id);

            if (performance == null)
                return NotFound();

            performances.Delete(performance);          
            return performance;
        }
    }
}
