using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class AudioService : IAudioService
    {
        private readonly IRepository<Audio> _audioRepository;

        public AudioService(IRepository<Audio> audioRepository)
        {
            _audioRepository = audioRepository;
        }
    }
}