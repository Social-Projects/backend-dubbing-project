using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class SpeechService : GenericService<Speech>, ISpeechService
    {
        private readonly IRepository<Performance> _performanceRepository;

        public SpeechService(IRepository<Speech> repository, IRepository<Performance> performanceRepository)
            : base(repository)
        {
            _performanceRepository = performanceRepository;
        }

        public override async Task CreateAsync(Speech entity)
        {
            var perf = await _performanceRepository.GetByIdAsync(entity.PerformanceId);

            entity.Performance = perf;

            await Repository.AddAsync(entity);
        }
    }
}