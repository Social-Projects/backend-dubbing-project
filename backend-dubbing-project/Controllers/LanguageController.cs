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
    [Route("api/language")]
    public class LanguageController : ControllerBase
    {
        IRepository<Language> languages;
        public LanguageController(IRepository<Language> languages)
        {
            this.languages = languages;
        }

        [HttpGet]
        public IEnumerable<Language> Get()
        {
            return languages.GetAllItems();
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Language> GetById(int id)
        {
            if (!languages.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return languages.GetItem(id);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Language> Create(Language language)
        {
            if (language == null)
                return BadRequest();
                
            languages.Create(language);
            return Ok(language);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Language> Delete(int id)
        {
            var list = languages.GetAllItems();
            var language = list.FirstOrDefault(x => x.Id == id);
            if (language == null)
                return NotFound();
            languages.Delete(language);
            return language;
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Language> Update(Language lenguage)
        {
            if (!languages.GetAllItems().Any(x => x.Id == lenguage.Id))
                return NotFound();
            languages.Update(lenguage);
            return lenguage;
        }
    }
}