using System.Collections.Generic;
using Dubbing.Models;

namespace Dubbing.Util
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllItems();
        T GetItem(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
