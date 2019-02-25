using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class SpeechService : GenericService<Speech>, ISpeechService
    {
        public SpeechService(IRepository<Speech> repository)
            : base(repository)
        {
        }
    }
}