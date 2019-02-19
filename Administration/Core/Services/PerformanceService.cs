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

        public PerformanceService(
            IRepository<Performance> performanceRepository,
            IRepository<Speech> speechRepository)
        {
            _performanceRepository = performanceRepository;
            _speechRepository = speechRepository;
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
                return await _speechRepository.List(s => s.PerformanceId == id);
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

        public async Task<Performance> DeleteAsync(Performance performance)
        {
            var performances = await _performanceRepository.List(p => p.Id == performance.Id);

            if (performances.Count() == 0)
            {
                return null;
            }
            else
            {
                await _performanceRepository.DeleteAsync(performance);
                return performance;
            }
        }
    }
}