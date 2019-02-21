using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var existedEntity = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(existedEntity).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}