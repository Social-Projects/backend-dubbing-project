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
    public class SpeechService : ISpeechService
    {
        private readonly IRepository<Speech> _speeches;

        public SpeechService(IRepository<Speech> speeches)
        {
            _speeches = speeches;
        }

        public async Task<List<Speech>> GetAllSpeeches()
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            return listOfSpeeches;
        }

        public async Task<Speech> GetSpeechById(int id)
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var speech = await _speeches.GetItemAsync(id);

            return speech;
        }

        public async Task<ICollection<Audio>> GetAudios(int id)
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var speech = await _speeches.GetItemAsync(id, source => source.Include(x => x.Audios));

            var audios = speech.Audios;

            return audios;
        }

        public async Task<Speech> Create(Speech model)
        {
            await _speeches.CreateAsync(model);

            return model;
        }

        public async Task<Speech> Update(Speech model)
        {
            var listOfSpeeches = await _speeches.GetAllItemsAsync();

            var doesNotExist = listOfSpeeches.All(x => x.Id != model.Id);

            if (doesNotExist)
                return null;

            await _speeches.UpdateAsync(model);

            return model;
        }

        public async Task<Speech> Delete(int id)
        {
            var list = await _speeches.GetAllItemsAsync(source => source.Include(x => x.Audios));

            var speech = list.FirstOrDefault(x => x.Id == id);

            foreach (var audio in speech.Audios)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory() + $@"\AudioFiles\", audio.FileName);
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

            await _speeches.DeleteAsync(speech);

            return speech;
        }
    }
}