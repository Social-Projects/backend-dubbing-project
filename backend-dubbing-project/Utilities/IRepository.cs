using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAllItems(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        T GetItem(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
