using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface ISpeechService
    {
        Task<List<Speech>> GetAllAsync();

        Task<Speech> GetByIdAsync(int id);

        Task CreateAsync(Speech entity);

        Task UpdateAsync(int id, Speech newEntity);

        Task DeleteAsync(int id);
    }
}