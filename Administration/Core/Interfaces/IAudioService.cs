using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IAudioService
    {
        Task<Audio> GetById(int id);

        Task<IEnumerable<Audio>> ListAllAsync();

        Task<IEnumerable<Audio>> ListAllAsync(Expression<Func<Audio, bool>> predicate);

        Task UploadAsync(Audio entity);

        Task AddAsync(Audio entity);

        Task UpdateAsync(Audio entity);

        Task DeleteAsync(Audio entity);

        //Task DeleteAllFromSpeech();
    }
}