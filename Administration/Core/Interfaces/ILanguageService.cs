using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAll();

        Task<Language> GetById(int id);

        Task Create(Language entity);

        Task Update(int id, Language newEntity);

        Task Delete(int id);
    }
}