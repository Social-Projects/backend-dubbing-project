using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/audio")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private IRepository<Audio> _audios;

        public AudioController(IRepository<Audio> audios)
        {
            _audios = audios;
        }

        /// <summary>
        /// Uploads a file and saves it to a local storage
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]AudioDTO model)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + $@"\Audio Files\", model.AudioFile.FileName);
            // Example of saving file to local root '~/wwwroot'
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.AudioFile.CopyToAsync(fileStream);
            }

            return Ok();
        }

        /// <summary>
        /// Removes a file from a local storage
        /// </summary>
        [HttpDelete("upload")]
        public ActionResult DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all audios
        /// </summary>
        /// <returns>Array of audios</returns>
        [HttpGet]
        public IEnumerable<Audio> Get()
        {
            return _audios.GetAllItems();
        }

        /// <summary>
        /// Get audios by id
        /// </summary>
        /// <returns>Audio with the following id</returns>
        /// <response code="200">Returns the audio with the following id</response>
        /// <response code="404">If the audio with the following id does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Audio> GetById(int id)
        {
            if (!_audios.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _audios.GetItem(id);
        }

        /// <summary>
        /// Creates a new audio
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created audio</returns>
        /// <response code="201">Returns the newly created audio</response>
        /// <response code="400">If the audio is not valid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Audio> Create(Audio model)
        {
            if (model == null)
                return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory() + $@"\Audio Files", model.FileName);
            var tfile = TagLib.File.Create(path);
            var duration = tfile.Properties.Duration;
            model.Duration = Convert.ToInt32(duration.TotalSeconds);
            _audios.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        /// <summary>
        /// Updates the audio
        /// </summary>
        /// <param name="model"></param>
        /// <returns>An updated audio</returns>
        /// <response code="200">Returns the updated audio</response>
        /// <response code="400">If the audio is not valid</response>
        /// <response code="404">If the audio with the following id does not exist</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Audio> Update(Audio model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!_audios.GetAllItems().Any(x => x.Id == model.Id))
                return NotFound();
            _audios.Update(model);
            return model;
        }

        /// <summary>
        /// Deletes the audio
        /// </summary>
        /// <param name="id">Audio id</param>
        /// <returns>Deleted audio</returns>
        /// <response code="200">Returns the deleted audio</response>
        /// <response code="404">If the audio with the following id does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Audio> Delete(int id)
        {
            var list = _audios.GetAllItems();
            var audio = list.FirstOrDefault(x => x.Id == id);

            if (audio == null)
                return NotFound();
            _audios.Delete(audio);
            return audio;
        }
    }
}
