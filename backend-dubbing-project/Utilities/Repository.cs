using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbSet<T> _entities;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<List<T>> GetAllItemsAsync(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _entities;

            if (includes != null)
            {
                query = includes(query);
            }

            var allItems = await query.ToListAsync();

            return allItems;
        }

        public async Task<T> GetItemAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = _entities.Where(x => x.Id == id);

            if (includes != null)
                query = includes(query);

            var item = await query.FirstOrDefaultAsync();

            return item;
        }

        public async Task CreateAsync(T entity)
        {
            entity.Id = default;

            _entities.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var exist = await _entities.FindAsync(entity.Id);

            _context.Entry(exist).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}