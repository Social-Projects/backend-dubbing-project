using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all languags.
        /// </summary>
        /// <returns>Array of languages.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Language>>> Get()
        {
            var listOfAllLanguages = await _languageService.GetAllLanguages();

            if (listOfAllLanguages.Count() == 0)
                return NotFound();

            return Ok(listOfAllLanguages);
        }

        /// <summary>
        /// Get language by id.
        /// </summary>
        /// <returns>Language with the following id.</returns>
        /// <response code="200">Returns the language with the following id.</response>
        /// <response code="404">If the language with the following id does not exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetById(int id)
        {
            var language = await _languageService.GetById(id);

            if (language == null)
                return NotFound();

            return Ok(language);
        }

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created language.</returns>
        /// <response code="201">Returns the newly created language.</response>
        /// <response code="400">If the language is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<Language>> Create(Language model)
        {
            if (model == null)
                return BadRequest();

            await _languageService.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        /// <summary>
        /// Updates the language.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        /// <response code="200">Returns No Content.</response>
        /// <response code="404">If the language not founded</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Language>> Delete(int id)
        {
            var lang = await _languageService.Delete(id);
            if (lang == null)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes the language.
        /// </summary>
        /// <param name="model">Language id.</param>
        /// <returns>No Content.</returns>
        /// <response code="200">Returns No Content.</response>
        /// <response code="404">If the language not founded</response>
        [HttpPut]
        public async Task<ActionResult<Language>> Update(Language model)
        {
            var lang = await _languageService.Update(model);
            if (lang == null)
                return NotFound();

            return NoContent();
        }
    }
}