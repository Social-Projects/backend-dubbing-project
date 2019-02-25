using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class AudioService : IAudioService
    {
        private readonly IRepository<Audio> _audioRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IRepository<Speech> _speechRepository;

        private readonly string _audioFilesFolderPath = Path.GetFullPath("../Web/AudioFiles/");

        public AudioService(IRepository<Audio> audioRepository, IFileRepository fileRepository, IRepository<Speech> speechRepository)
        {
            _audioRepository = audioRepository;
            _fileRepository = fileRepository;
            _speechRepository = speechRepository;
        }

        public async Task<Audio> GetById(int id)
        {
            return await _audioRepository.GetById(id);
        }

        public async Task<IEnumerable<Audio>> ListAllAsync()
        {
            return await _audioRepository.ListAllAsync();
        }

        public async Task<IEnumerable<Audio>> ListAllAsync(Expression<Func<Audio, bool>> predicate)
        {
            return await _audioRepository.List(predicate);
        }

        public async Task AddAsync(Audio entity)
        {
            Audio audio = await ChangeNameAndDuration(entity);

            await _audioRepository.AddAsync(audio);
        }

        public async Task UploadAsync(Audio entity)
        {
            string path = Path.Combine(_audioFilesFolderPath, entity.FileName);

            await _fileRepository.UploadAsync(entity, path);
        }

        public async Task UpdateAsync(Audio entity)
        {
            var fileToRemovePath = Path.Combine(_audioFilesFolderPath, entity.FileName);
            File.Delete(fileToRemovePath);
            Audio audio = await ChangeNameAndDuration(entity);

            await _audioRepository.UpdateAsync(audio);
        }

        public async Task DeleteAsync(Audio entity)
        {
            _fileRepository.Delete(entity.Speech.Audio, _audioFilesFolderPath);
        }

        private async Task<Audio> ChangeNameAndDuration(Audio entity)
        {
            var speech = await _speechRepository.GetById(entity.SpeechId);

            string newFileName = $"{speech.PerformanceId}_{entity.SpeechId}_{entity.LanguageId}.mp3";
            string oldPath = Path.Combine(_audioFilesFolderPath, entity.FileName + ".mp3");
            string newPath = Path.Combine(_audioFilesFolderPath, newFileName + ".mp3");
            File.Move(oldPath, newPath);

            var file = TagLib.File.Create(newPath);
            var duration = file.Properties.Duration;
            entity.Duration = Convert.ToInt32(duration.TotalSeconds);
            entity.FileName = newFileName;

            return entity;
        }
    }
}