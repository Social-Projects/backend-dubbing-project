using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.FileSystem
{
    public class FilesRepository : IFileRepository
    {
        public async Task UploadAsync(Audio audio, string path)
        {
            await File.WriteAllBytesAsync(path, audio.AudioFile);
        }

        public void Delete(IEnumerable<Audio> audios, string folderPath)
        {
            foreach (Audio audio in audios)
            {
                string audioPath = Path.Combine(folderPath, audio.FileName);

                File.Delete(audioPath);
            }
        }
    }
}