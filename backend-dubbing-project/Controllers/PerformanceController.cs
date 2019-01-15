using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [ApiController]
    [Route("api/performance")]
    public class PerformanceController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public PerformanceController()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Get all performances.
        /// </summary>
        /// <returns>Array of performances.</returns>
        [HttpGet]
        public IEnumerable<Performance> Get()
        {
            return _unitOfWork.Performances.GetAllItems();
        }

        /// <summary>
        /// Get performance by id.
        /// </summary>
        /// <returns>Performance with the following id.</returns>
        /// <response code="200">Returns the performance with the following id.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Performance> GetById(int id)
        {
            if (!_unitOfWork.Performances.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _unitOfWork.Performances.GetItem(id);
        }

        /// <summary>
        /// Creates a new performance.
        /// </summary>
        /// <param name="performance"></param>
        /// <returns>A newly created performance.</returns>
        /// <response code="201">Returns the newly created performance.</response>
        /// <response code="400">If the performance is not valid.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Performance>> Create(Performance performance)
        {
            _unitOfWork.Performances.Create(performance);
            await _unitOfWork.CommitAsync();
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
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Performance>> Update(Performance performance)
        {
            if (_unitOfWork.Performances.GetAllItems().All(x => x.Id != performance.Id))
                return NotFound();

            _unitOfWork.Performances.Update(performance);
            await _unitOfWork.CommitAsync();
            return performance;
        }

        /// <summary>
        /// Deletes the performance.
        /// </summary>
        /// <param name="id">Performance id.</param>
        /// <returns>Deleted performance.</returns>
        /// <response code="200">Returns the deleted performance.</response>
        /// <response code="404">If the performance with the following id does not exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Performance>> Delete(int id)
        {
            var list = _unitOfWork.Performances.GetAllItems();
            var performance = list.FirstOrDefault(x => x.Id == id);

            if (performance == null)
                return NotFound();

            _unitOfWork.Performances.Delete(performance);
            await _unitOfWork.CommitAsync();

            return performance;
        }
    }
}
