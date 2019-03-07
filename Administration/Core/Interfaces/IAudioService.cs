using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IAudioService
    {
        Task<List<Audio>> GetAllAsync();

        Task<Audio> GetByIdAsync(int id);

        Task CreateAsync(Audio entity);

        Task UploadAsync(Audio entity);

        Task UpdateAsync(int id, Audio newEntity);

        Task DeleteAsync(int id);
    }
}