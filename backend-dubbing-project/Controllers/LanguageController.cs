using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Controllers
{
    [ApiController]
    [Route("api/language")]
    public class LanguageController : ControllerBase
    {
        private IRepository<Language> _languages;

        public LanguageController(IRepository<Language> languages)
        {
            _languages = languages;
        }

        [HttpGet]
        public IEnumerable<Language> Get()
        {
            return _languages.GetAllItems();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Language> GetById(int id)
        {
            if (!_languages.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _languages.GetItem(id);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Language> Create(Language language)
        {
            if (language == null)
                return BadRequest();
            _languages.Create(language);
            return Ok(language);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Language> Delete(int id)
        {
            var list = _languages.GetAllItems();
            var language = list.FirstOrDefault(x => x.Id == id);
            if (language == null)
                return NotFound();
            _languages.Delete(language);
            return language;
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Language> Update(Language lenguage)
        {
            if (!_languages.GetAllItems().Any(x => x.Id == lenguage.Id))
                return NotFound();
            _languages.Update(lenguage);
            return lenguage;
        }
    }
}