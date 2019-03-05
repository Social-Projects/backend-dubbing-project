using System;
using System.IO;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using File = System.IO.File;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class AudioService : GenericService<Audio>, IAudioService
    {
        private readonly IFileRepository _fileRepository;

        private readonly IRepository<Speech> _speechRepository;

        private readonly string _audioFilesFolderPath = Path.GetFullPath("../Web/AudioFiles/");

        public AudioService(
            IRepository<Audio> repository,
            IRepository<Speech> speechRepository,
            IFileRepository fileRepository)
            : base(repository)
        {
            _speechRepository = speechRepository;
            _fileRepository = fileRepository;
        }

        public override async Task CreateAsync(Audio entity)
        {
            var audio = await ChangeNameAndDuration(entity);

            await Repository.AddAsync(audio);
        }

        public async Task UploadAsync(Audio entity)
        {
            var path = Path.Combine(_audioFilesFolderPath, entity.FileName);

            await _fileRepository.UploadAsync(entity, path);
        }

        public override async Task UpdateAsync(int id, Audio newEntity)
        {
            var fileToRemovePath = Path.Combine(_audioFilesFolderPath, newEntity.FileName);

            File.Delete(fileToRemovePath);

            newEntity = await ChangeNameAndDuration(newEntity);

            var oldEntity = await Repository.GetByIdAsync(id);

            if (oldEntity == null)
                throw new Exception($"{typeof(Audio)} entity with ID: {id} doesn't exist.");

            await Repository.UpdateAsync(oldEntity, newEntity);
        }

        public override async Task DeleteAsync(int id)
        {
            var audio = await Repository.GetByIdAsync(id);

            _fileRepository.Delete(audio.Speech.Audios, _audioFilesFolderPath);
        }

        private async Task<Audio> ChangeNameAndDuration(Audio entity)
        {
            var speech = await _speechRepository.GetByIdAsync(entity.SpeechId);

            var newFileName = $"{speech.PerformanceId}_{entity.SpeechId}_{entity.LanguageId}.mp3";
            var oldPath = Path.Combine(_audioFilesFolderPath, entity.FileName + ".mp3");
            var newPath = Path.Combine(_audioFilesFolderPath, newFileName + ".mp3");
            File.Move(oldPath, newPath);

            var file = TagLib.File.Create(newPath);
            var duration = file.Properties.Duration;
            entity.Duration = Convert.ToInt32(duration.TotalSeconds);
            entity.FileName = newFileName;

            return entity;
        }
    }
}