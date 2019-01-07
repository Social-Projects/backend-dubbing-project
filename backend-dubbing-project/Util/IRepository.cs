using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dubbing_project.Util
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllItems();
        T GetItem(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
