using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dubbing.Models;
namespace Dubbing.Util
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
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
            return _entities.AsEnumerable<T>();
        }

        public T GetItem(int id)
        {
            return _entities.Find(id);
        }

        public void Create(T entity)
        {
            entity.Id = default(int);
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            T exist = _entities.Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);  
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
