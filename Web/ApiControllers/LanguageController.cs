using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using Web.ViewModels;

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
        public async Task<ActionResult<List<LanguageViewModel>>> Get()
        {
            var listOfAllLanguages = await _languageService.GetAllLanguagesAsync();

            var mappedLanguages = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageViewModel>>(listOfAllLanguages);

            return mappedLanguages.ToList();
        }

        /// <summary>
        /// Get language by id.
        /// </summary>
        /// <returns>Language with the following id.</returns>
        /// <response code="200">Returns the language with the following id.</response>
        /// <response code="404">If the language with the following id does not exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageViewModel>> GetById(int id)
        {
            var language = await _languageService.GetByIdAsync(id);

            if (language == null)
                return NotFound();

            var mappedLanguage = _mapper.Map<Language, LanguageViewModel>(language);

            return mappedLanguage;
        }

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>A newly created language.</returns>
        /// <response code="201">Returns the newly created language.</response>
        /// <response code="400">If the language is not valid.</response>
        [HttpPost]
        public async Task<ActionResult<LanguageViewModel>> Create(LanguageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<LanguageViewModel, Language>(viewModel);

                await _languageService.CreateAsync(model);

                var mappedModel = _mapper.Map<Language, LanguageViewModel>(model);

                return CreatedAtAction(nameof(GetById), new { id = mappedModel.Id }, mappedModel);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates the language.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        /// <response code="200">Returns No Content.</response>
        /// <response code="404">If the language not founded</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var lang = await _languageService.DeleteAsync(id);
            if (lang == null)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes the language.
        /// </summary>
        /// <param name="viewModel">Language id.</param>
        /// <returns>No Content.</returns>
        /// <response code="200">Returns No Content.</response>
        /// <response code="404">If the language not founded</response>
        [HttpPut]
        public async Task<ActionResult> Update(LanguageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<LanguageViewModel, Language>(viewModel);
                var lang = await _languageService.UpdateAsync(model);

                if (lang == null)
                    return NotFound();
                else
                    return NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}