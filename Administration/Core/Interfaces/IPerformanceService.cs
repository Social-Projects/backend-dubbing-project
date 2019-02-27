using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IPerformanceService
    {
        Task<Performance> GetById(int id);

        Task<List<Speech>> GetChildrenById(int id);

        Task<List<Performance>> GetAll();

        Task Create(Performance entity);

        Task Update(int id, Performance newEntity);

        Task Delete(int id);
    }
}