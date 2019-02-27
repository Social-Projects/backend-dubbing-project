using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DubbingContext _dbContext;

        public Repository(DubbingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext
                .Set<T>()
                .FindAsync(id);
        }

        public async Task<T> GetByIdWithChildren(int id, string childrenName)
        {
            return await _dbContext
                .Set<T>()
                .Include(childrenName)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<List<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext
                .Set<T>()
                .Include(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            _dbContext
                .Set<T>()
                .Add(entity);

            await _dbContext
                .SaveChangesAsync();
        }

        public async Task UpdateAsync(T oldEntity, T newEntity)
        {
            _dbContext
                .Entry(oldEntity)
                .CurrentValues
                .SetValues(newEntity);

            await _dbContext
                .SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext
                .Set<T>()
                .Remove(entity);

            await _dbContext
                .SaveChangesAsync();
        }
    }
}