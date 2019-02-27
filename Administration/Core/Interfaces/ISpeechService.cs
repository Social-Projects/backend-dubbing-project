using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface ISpeechService
    {
        Task<List<Speech>> GetAll();

        Task<Speech> GetById(int id);

        Task Create(Speech entity);

        Task Update(int id, Speech newEntity);

        Task Delete(int id);
    }
}