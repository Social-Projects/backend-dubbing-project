using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.ApplicationCore.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> ListAllAsync();

        Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}