using System.Collections.Generic;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAllItems();

        T GetItem(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
