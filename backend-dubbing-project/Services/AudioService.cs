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
    public class AudioService : IAudioService
    {
        private readonly IRepository<Audio> _audios;
        private readonly IRepository<Speech> _speeches;

        public AudioService(IRepository<Audio> audios, IRepository<Speech> speeches)
        {
            _audios = audios;
            _speeches = speeches;
        }

        public async Task<List<Audio>> GetAllAudios()
        {
            var listOfAllAudios = await _audios.GetAllItemsAsync();

            return listOfAllAudios;
        }

        public async Task<Audio> GetAudioById(int id)
        {
            var listOfAllAudios = await _audios.GetAllItemsAsync();

            var doesNotExist = listOfAllAudios.All(x => x.Id != id);

            if (doesNotExist)
                return null;

            var audio = await _audios.GetItemAsync(id);

            return audio;
        }

        public async Task<AudioDTO> Upload(AudioDTO audio)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles\", audio.AudioFile.FileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
                await audio.AudioFile.CopyToAsync(fileStream);

            return audio;
        }

        public async Task<Audio> CreateAudio(Audio audio)
        {
            var currentSpeech = await _speeches.GetItemAsync(audio.SpeechId);

            var newFileName = $"{currentSpeech.PerformanceId}_{audio.SpeechId}_{audio.LanguageId}.mp3";

            var oldPath = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles", audio.FileName);

            var newPath = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles", newFileName);

            File.Move(oldPath, newPath);

            var tfile = TagLib.File.Create(newPath);

            var duration = tfile.Properties.Duration;

            audio.Duration = Convert.ToInt32(duration.TotalSeconds);

            audio.FileName = newFileName;

            await _audios.CreateAsync(audio);

            return audio;
        }

        public async Task<Audio> UpdateAudio(Audio entity)
        {
            var listOfAllAudios = await _audios.GetAllItemsAsync();

            var doesNotExist = listOfAllAudios.All(x => x.Id != entity.Id);

            if (doesNotExist)
                return null;

            var audio = await _audios.GetItemAsync(entity.Id, source => source.Include(x => x.Speech));

            var newFileName = $"{audio.Speech.PerformanceId}_{entity.SpeechId}_{entity.LanguageId}.mp3";

            if (newFileName == entity.FileName)
                return null;

            var fileToRemovePath = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles", audio.FileName);

            File.Delete(fileToRemovePath);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles", entity.FileName);

            var newPath = Path.Combine(Directory.GetCurrentDirectory() + @"\AudioFiles", newFileName);

            File.Move(oldPath, newPath);

            var tfile = TagLib.File.Create(newPath);

            var duration = tfile.Properties.Duration;

            entity.Duration = Convert.ToInt32(duration.TotalSeconds);

            entity.FileName = newFileName;

            await _audios.UpdateAsync(entity);

            return entity;
        }
    }
}