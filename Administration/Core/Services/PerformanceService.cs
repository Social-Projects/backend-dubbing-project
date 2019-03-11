using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class PerformanceService : GenericService<Performance>, IPerformanceService
    {
        private readonly IAudioService _audioService;

        public PerformanceService(IRepository<Performance> repository, IAudioService audioService)
            : base(repository)
        {
            _audioService = audioService;
        }

        public async Task<List<Speech>> GetChildrenByIdAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            return performance?.Speeches.ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            if (performance == null)
                return;

            foreach (var speech in performance.Speeches)
            {
                await _audioService.DeleteAsync(speech.Id);
            }

            await Repository.DeleteAsync(performance);
        }
    }
}