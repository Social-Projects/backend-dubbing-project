using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IAudioService
    {
        Task<List<Audio>> GetAll();

        Task<Audio> GetById(int id);

        Task Create(Audio entity);

        Task UploadAsync(Audio entity);

        Task Update(int id, Audio newEntity);
    }
}