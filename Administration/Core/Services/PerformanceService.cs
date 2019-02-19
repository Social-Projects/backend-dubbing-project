using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class PerformanceService : IPerformanceService
    {
        private IRepository<Performance> _performanceRepository;

        public PerformanceService(IRepository<Performance> performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }
    }
}