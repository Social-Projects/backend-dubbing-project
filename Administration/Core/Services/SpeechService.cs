using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly IRepository<Speech> _speechRepository;

        public SpeechService(IRepository<Speech> speechRepository)
        {
            _speechRepository = speechRepository;
        }
    }
}