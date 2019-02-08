using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Language> GetById(int id)
        {
            if (!_languages.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _languages.GetItem(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Language> Create(Language language)
        {
            if (language == null)
                return BadRequest();

            _languages.Create(language);
            return Ok(language);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Language> Delete(int id)
        {
            var list = _languages.GetAllItems(source => source.Include(x => x.Audios));
            var language = list.FirstOrDefault(x => x.Id == id);
            if (language == null)
                return NotFound();

            foreach (var audio in language.Audios)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory() + @"\Audio Files\", audio.FileName);
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

            _languages.Delete(language);
            return language;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Language> Update(Language lenguage)
        {
            if (!_languages.GetAllItems().Any(x => x.Id == lenguage.Id))
                return NotFound();
            _languages.Update(lenguage);
            return lenguage;
        }
    }
}