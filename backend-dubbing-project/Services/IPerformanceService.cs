using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public interface IPerformanceService
    {
        Task<ICollection<Speech>> GetSpeeches(int id);

        Task<List<Performance>> GetAllPerformances();

        Task<Performance> GetPerformanceById(int id);

        Task<Performance> CreatePerformance(Performance performance);

        Task<Performance> UpdatePerformance(Performance performance);

        Task<Performance> DeletePerformance(int id);
    }
}