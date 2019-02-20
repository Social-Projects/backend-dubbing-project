using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRepository<Performance> _performanceRepository;
        private readonly IRepository<Speech> _speechRepository;
        private readonly IRepository<Audio> _audioRepository;

        public PerformanceService(
            IRepository<Performance> performanceRepository,
            IRepository<Speech> speechRepository,
            IRepository<Audio> audioRepository)
        {
            _performanceRepository = performanceRepository;
            _speechRepository = speechRepository;
            _audioRepository = audioRepository;
        }

        public async Task<IEnumerable<Performance>> GetAllAsync()
        {
            return await _performanceRepository.ListAllAsync();
        }

        public async Task<IEnumerable<Speech>> GetSpeechesAsync(int id)
        {
            var performance = await _performanceRepository.List(p => p.Id == id);

            if (performance.Count() == 0)
            {
                return null;
            }
            else
            {
                var speeches = await _speechRepository.List(s => s.PerformanceId == id);

                foreach (var speech in speeches)
                {
                    var audios = await _audioRepository.List(a => a.SpeechId == speech.Id);
                    if (audios.Count() != 0)
                        speech.Duration = audios.Max(a => a.Duration);
                }

                return speeches;
            }
        }

        public async Task<Performance> GetByIdAsync(int id)
        {
            return await _performanceRepository.GetById(id);
        }

        public async Task CreateAsync(Performance performance)
        {
            await _performanceRepository.AddAsync(performance);
        }

        public async Task<Performance> UpdateAsync(Performance performance)
        {
            var performances = await _performanceRepository.List(p => p.Id == performance.Id);

            if (performances.Count() == 0)
            {
                return null;
            }
            else
            {
                await _performanceRepository.UpdateAsync(performance);
                return performance;
            }
        }

        public async Task<Performance> DeleteAsync(int id)
        {
            var performances = await _performanceRepository.List(p => p.Id == id);

            if (performances.Count() == 0)
            {
                return null;
            }
            else
            {
                await _performanceRepository.DeleteAsync(performances.First());
                return performances.First();
            }
        }
    }
}