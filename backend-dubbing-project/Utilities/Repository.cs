using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> _entities;
        private DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllItems()
        {
            return _entities;
        }

        public T GetItem(int id)
        {
            return _entities.Find(id);
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }
    }
}
