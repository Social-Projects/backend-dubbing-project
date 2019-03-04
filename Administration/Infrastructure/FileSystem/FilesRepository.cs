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

        public void Unload(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void Delete(IEnumerable<Audio> audios, string folderPath)
        {
            foreach (var audio in audios)
            {
                var audioPath = Path.Combine(folderPath, audio.FileName);

                File.Delete(audioPath);
            }
        }
    }
}