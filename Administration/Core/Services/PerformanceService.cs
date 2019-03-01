using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class PerformanceService : GenericService<Performance>, IPerformanceService
    {
        public PerformanceService(IRepository<Performance> repository)
            : base(repository)
        {
        }

        public async Task<List<Speech>> GetChildrenByIdAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            return performance?.Speeches.ToList();
        }
    }
}