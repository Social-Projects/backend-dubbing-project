using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAllLanguages();

        Task<Language> GetById(int id);

        Task Create(Language language);

        Task<Language> Update(Language language);

        Task<Language> Delete(int id);
    }
}