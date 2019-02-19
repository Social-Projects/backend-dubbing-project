using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IAdminService
    {
        Admin Authenticate(string username, string password);

        IEnumerable<Admin> GetAll();

        Admin GetById(int id);

        Admin Create(Admin user, string password);

        void Update(Admin user, string password = null);

        void Delete(int id);
    }
}