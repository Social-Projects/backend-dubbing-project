using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    public interface IFileRepository
    {
        Task UploadAsync(Audio audio, string path);

        void Unload(string path);

        void Delete(IEnumerable<Audio> audios, string path);
    }
}