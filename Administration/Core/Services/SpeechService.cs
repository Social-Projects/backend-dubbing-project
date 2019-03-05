using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class SpeechService : GenericService<Speech>, ISpeechService
    {
        private readonly IRepository<Performance> _performanceRepository;

        public SpeechService(IRepository<Speech> repository, IRepository<Performance> performanceRepository)
            : base(repository)
        {
            _performanceRepository = performanceRepository;
        }

        public async Task<List<Audio>> GetChildrenByIdAsync(int id)
        {
            var speech = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            return speech?.Audios.ToList();
        }
    }
}