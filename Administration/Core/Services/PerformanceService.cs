using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class PerformanceService : GenericService<Performance>, IPerformanceService
    {
        private readonly ISpeechService _speechService;
        private readonly IFileRepository _fileRepository;
        private readonly string _audioFilesFolderPath = Path.GetFullPath("../Web/AudioFiles/");

        public PerformanceService(
            IRepository<Performance> repository,
            ISpeechService speechService,
            IFileRepository fileRepository)
            : base(repository)
        {
            _speechService = speechService;
            _fileRepository = fileRepository;
        }

        public async Task<List<Speech>> GetChildrenByIdAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            if (performance != null)
            {
                foreach (var speech in performance.Speeches)
                {
                    var speechAudios = await _speechService.GetChildrenByIdAsync(speech.Id);
                    speech.Duration = speechAudios.Max(a => a.Duration);
                }
            }

            return performance?.Speeches.ToList();
        }

        public async override Task DeleteAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            if (performance != null)
            {
                foreach (var speechId in performance.Speeches.Select(s => s.Id))
                {
                    var audios = await _speechService.GetChildrenByIdAsync(speechId);
                    _fileRepository.Delete(audios, _audioFilesFolderPath);
                }

                await Repository.DeleteAsync(performance);
            }
        }
    }
}