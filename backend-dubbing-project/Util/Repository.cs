using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dubbing.Models;
namespace Dubbing.Util
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private DbContext context;
        private DbSet<T> entities;
        public Repository(DbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllItems()
        {
            return this.entities.AsEnumerable<T>();
        }

        public T GetItem(int id)
        {
            return this.entities.Find(id);
        }

        public void Create(T entity)
        {
            entity.Id = default(int);
            this.entities.Add(entity);
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            T exist = this.entities.Find(entity.Id);
            this.context.Entry(exist).CurrentValues.SetValues(entity);  
  
            this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.entities.Remove(entity);
            this.context.SaveChanges();
        }
    }
}
