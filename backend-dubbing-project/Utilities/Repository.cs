using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbSet<T> _entities;
        private DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllItems(params string[] includes)
        {
            IQueryable<T> query = _entities;
            query.Include(includes[0]);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.AsEnumerable();
        }

        public T GetItem(int id, params string[] includes)
        {
            IQueryable<T> query = _entities.Where(x => x.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
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
