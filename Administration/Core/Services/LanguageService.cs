using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class LanguageService : ILanguageService
    {
        private IRepository<Language> _languageRepository;

        private IRepository<Audio> _audioRepository;

        private readonly IAudioService _audioService;

        public LanguageService(IRepository<Language> languageRepository, IRepository<Audio> audioRepository, IAudioService audioService)
        {
            _languageRepository = languageRepository;
            _audioRepository = audioRepository;
            _audioService = audioService;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            var listOfAllLanguages = await _languageRepository.ListAllAsync();

            return listOfAllLanguages;
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            var language = await _languageRepository.GetById(id);

            return language;
        }

        public async Task CreateAsync(Language language)
        {
            await _languageRepository.AddAsync(language);
        }

        public async Task<Language> UpdateAsync(Language language)
        {
            if (_languageRepository.GetById(language.Id) == null)
                return null;

            await _languageRepository.UpdateAsync(language);

            return language;
        }

        public async Task<Language> DeleteAsync(int id)
        {
            var langById = await _languageRepository.GetById(id);

            if (langById == null)
                return null;

            foreach (var audio in langById.Audios)
            {
                await _audioService.DeleteAsync(audio);
            }

            await _languageRepository.DeleteAsync(langById);

            return langById;
        }
    }
}