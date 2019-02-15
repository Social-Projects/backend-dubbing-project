using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languages;

        public LanguageService(IRepository<Language> languages)
        {
            _languages = languages;
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            return listOfAllLanguages;
        }

        public async Task<Language> GetById(int id)
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            var doesNotExist = listOfAllLanguages.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var language = await _languages.GetItemAsync(id);

            return language;
        }

        public async Task Create(Language language)
        {
            await _languages.CreateAsync(language);
        }

        public async Task<Language> Update(Language language)
        {
            var listOfAllLanguages = await _languages.GetAllItemsAsync();

            var doesNotExist = listOfAllLanguages.All(x => x.Id != language.Id);

            if (doesNotExist)
                return null;

            await _languages.UpdateAsync(language);

            return language;
        }

        public async Task<Language> Delete(int id)
        {
            var list = await _languages.GetAllItemsAsync(source => source.Include(x => x.Audios));

            var language = list.FirstOrDefault(x => x.Id == id);

            if (language == null)
                return null;

            foreach (var audio in language.Audios)
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

            await _languages.DeleteAsync(language);

            return language;
        }
    }
}