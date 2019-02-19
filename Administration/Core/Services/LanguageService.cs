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

        public LanguageService(IRepository<Language> languageRepository, IRepository<Audio> audioRepository)
        {
            _languageRepository = languageRepository;
            _audioRepository = audioRepository;
        }

        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            var listOfAllLanguages = await _languageRepository.ListAllAsync();

            return listOfAllLanguages;
        }

        public async Task<Language> GetById(int id)
        {
            var listOfAllLanguages = await _languageRepository.ListAllAsync();

            if (!listOfAllLanguages.Any(x => x.Id == id))
                return null;

            var language = await _languageRepository.GetById(id);

            return language;
        }

        public async Task Create(Language language)
        {
            await _languageRepository.AddAsync(language);
        }

        public async Task<Language> Update(Language language)
        {
            var listOfAllLanguages = await _languageRepository.ListAllAsync();

            if (!listOfAllLanguages.Any(x => x.Id == language.Id))
                return null;

            await _languageRepository.UpdateAsync(language);

            return language;
        }

        public async Task<Language> Delete(int id)
        {
            var list = await _languageRepository.ListAllAsync();

            var language = list.FirstOrDefault(x => x.Id == id);

            if (language == null)
                return null;

            var audioList = await _audioRepository.ListAllAsync();

            var audioListWithLangId = audioList.Where(x => x.Language.Id == id);

            foreach (var audio in audioListWithLangId)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles\", audio.FileName);
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
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

            await _languageRepository.DeleteAsync(language);

            return language;
        }
    }
}