using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : Controller
    {
        private readonly IAudioService _audioService;

        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

//        /// <summary></summary>
//        /// <response code="200"> </response>
//        [HttpGet]
//        public async Task<ActionResult<List<Audio>>> GetAll()
//        {
//            return Ok();
//        }
//
//        /// <summary></summary>
//        /// <param name="id"></param>
//        /// <response code="200"></response>
//        [HttpGet]
//        public async Task<ActionResult<Audio>> GetById(int id)
//        {
//            return Ok();
//        }
//
//        /// <summary></summary>
//        /// <param name="model"></param>
//        /// <response code="204"></response>
//        [HttpPost]
//        public async Task<ActionResult> Create(Audio model)
//        {
//            return NoContent();
//        }
//
//        /// <summary></summary>
//        /// <param name="model"></param>
//        /// <response code="201"></response>
//        [HttpPost]
//        public async Task<ActionResult> Upload(Audio model)
//        {
//            return CreatedAtAction("Create", model);
//        }
//
//        /// <summary></summary>
//        /// <param name="id"></param>
//        /// <param name="model"></param>
//        /// <response code="204"></response>
//        /// <response code="400"></response>
//        [HttpPut]
//        public async Task<ActionResult> Update(int id, Audio model)
//        {
//            if (id != model.Id)
//                return BadRequest();
//
//            return NoContent();
//        }
    }
}