using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [Route("api/speech")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        private IRepository<Speech> _speeches;

        public SpeechController(IRepository<Speech> speeches)
        {
            _speeches = speeches;
        }

        [HttpGet]
        public IEnumerable<Speech> Get()
        {
            return _speeches.GetAllItems();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Speech> GetById(int id)
        {
            if (!_speeches.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _speeches.GetItem(id);
        }

        [HttpGet("{id}/audios")]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<Audio>> GetAudios(int id)
        {
            if (!_speeches.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return Ok(_speeches.GetItem(id, source => source.Include(x => x.Audios)).Audios);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Speech> Create(Speech model)
        {
            _speeches.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Speech> Update(Speech model)
        {
            if (!_speeches.GetAllItems().Any(x => x.Id == model.Id))
                return NotFound();

            _speeches.Update(model);
            return model;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Speech> Delete(int id)
        {
            var list = _speeches.GetAllItems();
            var speech = list.FirstOrDefault(x => x.Id == id);

            if (speech == null)
                return NotFound();

            _speeches.Delete(speech);
            return speech;
        }
    }
}