using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        /// <summary>Controller method for getting a list of all languages.</summary>
        /// <returns>List of all languages.</returns>
        /// <response code="200">Is returned when the list has at least one language.</response>
        /// <response code="404">Is returned when the list of languages is empty.</response>
        [HttpGet]
        public async Task<ActionResult<List<LanguageDTO>>> GetAll()
        {
            var listOfLanguages = await _languageService.GetAllAsync();

            var listOfLanguageDTOs = _mapper.Map<List<Language>, List<LanguageDTO>>(listOfLanguages);

            return Ok(listOfLanguageDTOs);
        }

        /// <summary>Controller method for getting a language by id.</summary>
        /// <param name="id">Id of language that need to receive.</param>
        /// <returns>The language with the following id.</returns>
        /// <response code="200">Is returned when language does exist.</response>
        /// <response code="404">Is returned when language with such Id doesn't exist.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageDTO>> GetById(int id)
        {
            var language = await _languageService.GetByIdAsync(id);

            if (language == null)
                return NotFound();

            var languageDTO = _mapper.Map<Language, LanguageDTO>(language);

            return Ok(languageDTO);
        }

        /// <summary>Controller method for creating new language.</summary>
        /// <param name="languageDTO">Language model which needed to create.</param>
        /// <returns>Status code and Language.</returns>
        /// <response code="201">Is returned when language is successfully created.</response>
        /// <response code="400">Is returned when invalid data is passed.</response>
        /// <response code="409">Is returned when language with such parameters already exists.</response>
        [HttpPost]
        public async Task<ActionResult<LanguageDTO>> Create(LanguageDTO languageDTO)
        {
            var language = _mapper.Map<LanguageDTO, Language>(languageDTO);

            await _languageService.CreateAsync(language);

            var newLangDTO = _mapper.Map<Language, LanguageDTO>(language);

            return CreatedAtAction(nameof(GetById), new {id = newLangDTO.Id}, newLangDTO);
        }

        /// <summary>Controller method for updating an already existing language with following id.</summary>
        /// <param name="id">Id of the language that is needed to be updated.</param>
        /// <param name="languageDTO">The language model that is needed to be created.</param>
        /// <returns>Status code and optionally exception message.</returns>
        /// <response code="204">Is returned when language is successfully updated.</response>
        /// <response code="400">Is returned when language with or invalid data is passed.</response>
        /// <response code="404">Is returned when language with such Id is not founded</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, LanguageDTO languageDTO)
        {
            if (languageDTO.Id != id)
                BadRequest();

            var language = _mapper.Map<LanguageDTO, Language>(languageDTO);

            await _languageService.UpdateAsync(id, language);

            return NoContent();
        }

        /// <summary>Controller method for deleting an already existing language with following id.</summary>
        /// <param name="id">Id of the language that needed to delete.</param>
        /// <returns>Status code</returns>
        /// <response code="204">Is returned when language is successfully deleted.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _languageService.DeleteAsync(id);

            return NoContent();
        }
    }
}