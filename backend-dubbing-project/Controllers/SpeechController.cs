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
    [Route("api/speech")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        private readonly IRepository<Speech> _speeches;

        public SpeechController(IRepository<Speech> speeches)
        {
            _speeches = speeches;
        }

        [HttpGet]
        public async Task<ActionResult<List<Speech>>> Get()
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            return Ok(listOfSpeeches);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Speech>> GetById(int id)
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != id);

            if (doesNotExist)
                return NotFound();

            var speech = await _speeches.GetItemAsync(id);

            return Ok(speech);
        }

        [HttpGet("{id}/audios")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Audio>>> GetAudios(int id)
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != id);

            if (doesNotExist)
                return NotFound();

            var speech = await _speeches.GetItemAsync(id, source => source.Include(x => x.Audios));

            var audios = speech.Audios;

            return Ok(audios);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Speech>> Create(Speech model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _speeches.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Speech>> Update(Speech model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != model.Id);

            if (doesNotExist)
                return NotFound();

            await _speeches.UpdateAsync(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Speech>> Delete(int id)
        {
            var list = await _speeches.GetAllItemsAsync(source => source.Include(x => x.Audios));

            var speech = list.FirstOrDefault(x => x.Id == id);

            foreach (var audio in speech.Audios)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory() + $@"\Audio Files\", audio.FileName);
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

            foreach (var audio in speech.Audios)
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

            await _speeches.DeleteAsync(speech);

            return NoContent();
        }
    }
}