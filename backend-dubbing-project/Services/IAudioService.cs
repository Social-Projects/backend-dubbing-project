using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Services
{
    public interface IAudioService
    {
        Task<List<Audio>> GetAllAudios();

        Task<Audio> GetAudioById(int id);

        Task<AudioDTO> Upload(AudioDTO audio);

        Task<Audio> CreateAudio(Audio audio);

        Task<Audio> UpdateAudio(Audio entity);
    }
}