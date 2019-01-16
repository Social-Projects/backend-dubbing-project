using System.Collections.Generic;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAllItems(params string[] includes);

        T GetItem(int id, params string[] includes);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
