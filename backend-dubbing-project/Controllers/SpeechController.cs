using System;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Speech> GetById(int id)
        {
            if (!_speeches.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return _speeches.GetItem(id);
        }

        [HttpGet("{id}/audios")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Audio>> GetAudios(int id)
        {
            if (!_speeches.GetAllItems().Any(x => x.Id == id))
                return NotFound();

            return Ok(_speeches.GetItem(id, source => source.Include(x => x.Audios)).Audios);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Speech> Create(Speech model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _speeches.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Speech> Update(Speech model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_speeches.GetAllItems().Any(x => x.Id == model.Id))
                return NotFound();

            _speeches.Update(model);
            return model;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Speech> Delete(int id)
        {
            var list = _speeches.GetAllItems(source => source.Include(x => x.Audios));
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

            _speeches.Delete(speech);
            return speech;
        }
    }
}