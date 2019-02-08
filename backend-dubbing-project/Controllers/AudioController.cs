using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/audio")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private IRepository<Audio> _audios;
        private IRepository<Speech> _speeches;

        public AudioController(IRepository<Audio> audios, IRepository<Speech> speeches)
        {
            _audios = audios;
            _speeches = speeches;
        }

        /// <summary>
        /// Uploads a file and saves it to a local storage
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]AudioDTO model)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files\", model.AudioFile.FileName);
            // Example of saving file to local root '~/wwwroot'
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.AudioFile.CopyToAsync(fileStream);
            }

            return Ok();
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
            var curSpeech = _speeches.GetItem(model.SpeechId);
            string newFileName = $"{curSpeech.PerformanceId}_{model.SpeechId}_{model.LanguageId}.mp3";
            var oldPath = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files", model.FileName);
            var newPath = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files", newFileName);
            System.IO.File.Move(oldPath, newPath);
            var tfile = TagLib.File.Create(newPath);
            var duration = tfile.Properties.Duration;
            model.Duration = Convert.ToInt32(duration.TotalSeconds);
            model.FileName = newFileName;
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
            if (!_audios.GetAllItems().Any(x => x.Id == model.Id))
                return NotFound();

            var audio = _audios.GetItem(model.Id, source => source.Include(x => x.Speech));

            string newFileName = $"{audio.Speech.PerformanceId}_{model.SpeechId}_{model.LanguageId}.mp3";

            if (newFileName == model.FileName)
                return model;

            var fileToRemovePath = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files", audio.FileName);
            System.IO.File.Delete(fileToRemovePath);
            var oldPath = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files", model.FileName);
            var newPath = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files", newFileName);
            System.IO.File.Move(oldPath, newPath);
            var tfile = TagLib.File.Create(newPath);
            var duration = tfile.Properties.Duration;
            model.Duration = Convert.ToInt32(duration.TotalSeconds);
            model.FileName = newFileName;
            _audios.Update(model);

            return model;
        }
    }
}
