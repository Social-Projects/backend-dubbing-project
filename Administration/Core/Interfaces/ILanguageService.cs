using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguages();

        Task<Language> GetById(int id);

        Task Create(Language language);

        Task<Language> Update(Language language);

        Task<Language> Delete(int id);
    }
}