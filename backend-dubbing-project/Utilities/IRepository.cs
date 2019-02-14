using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllItemsAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        Task<T> GetItemAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
