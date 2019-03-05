using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class LanguageService : GenericService<Language>, ILanguageService
    {
        public LanguageService(IRepository<Language> repository)
            : base(repository)
        {
        }
    }
}