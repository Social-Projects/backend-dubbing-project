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
        private readonly IRepository<Language> _languages;

        public LanguageController(IRepository<Language> languages)
        {
            _languages = languages;
        }

        [HttpGet]
        public async Task<ActionResult<List<Language>>> Get()
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            return Ok(listOfAllLanguages);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Language>> GetById(int id)
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            var doesNotExist = listOfAllLanguages.All(x => x.Id != id);

            if (doesNotExist)
                return NotFound();

            var language = await _languages.GetItemAsync(id);

            return Ok(language);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Language>> Create(Language language)
        {
            if (language == null)
                return BadRequest();

            await _languages.CreateAsync(language);

            return CreatedAtAction(nameof(GetById), new { id = language.Id }, language);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Language>> Delete(int id)
        {
            var list = await _languages.GetAllItemsAsync(source => source.Include(x => x.Audios));

            var language = list.FirstOrDefault(x => x.Id == id);

            if (language == null)
                return NotFound();

            foreach (var audio in language.Audios)
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

            await _languages.DeleteAsync(language);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Language>> Update(Language language)
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            var doesNotExist = listOfAllLanguages.All(x => x.Id != language.Id);

            if (doesNotExist)
                return NotFound();

            await _languages.UpdateAsync(language);

            return NoContent();
        }
    }
}