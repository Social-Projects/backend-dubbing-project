using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class SpeechService : GenericService<Speech>, ISpeechService
    {
        private readonly IFileRepository _fileRepository;
        private readonly string _audioFilesFolderPath = Path.GetFullPath("../Web/AudioFiles/");

        public SpeechService(IRepository<Speech> repository, IFileRepository fileRepository)
            : base(repository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<List<Audio>> GetChildrenByIdAsync(int id)
        {
            var speech = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            return speech?.Audios.ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            var speech = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            if (speech != null)
            {
                _fileRepository.Delete(speech.Audios, _audioFilesFolderPath);
                await Repository.DeleteAsync(speech);
            }
        }
    }
}