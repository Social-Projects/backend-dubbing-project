using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface IFileRepository
    {
        Task UploadAsync(Audio audio, string path);

        void Delete(IEnumerable<Audio> audios, string path);
    }
}