using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class LanguageService : ILanguageService
    {
        private IRepository<Language> _languageRepository;

        public LanguageService(IRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }
    }
}