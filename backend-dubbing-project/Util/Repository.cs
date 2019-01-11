using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dubbing.Util
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _entities;
        public Repository(DbContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllItems()
        {
            return this._entities;
        }

        public T GetItem(int id)
        {
            return this._entities.Find(id);
        }

        public void Create(T entity)
        {
            this._entities.Add(entity);
        }

        public void Update(T entity)
        {
            this._entities.Update(entity);
        }

        public void Delete(T entity)
        {
            this._entities.Remove(entity);
        }
    }
}
