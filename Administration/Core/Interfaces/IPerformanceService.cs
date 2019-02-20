using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IPerformanceService
    {
        Task<IEnumerable<Performance>> GetAllAsync();

        Task<IEnumerable<Speech>> GetSpeechesAsync(int id);

        Task<Performance> GetByIdAsync(int id);

        Task CreateAsync(Performance performance);

        Task<Performance> UpdateAsync(Performance performance);

        Task<Performance> DeleteAsync(int id);
    }
}