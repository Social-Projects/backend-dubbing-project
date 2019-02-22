using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();

        Task<Language> GetByIdAsync(int id);

        Task CreateAsync(Language language);

        Task<Language> UpdateAsync(Language language);

        Task<Language> DeleteAsync(int id);
    }
}