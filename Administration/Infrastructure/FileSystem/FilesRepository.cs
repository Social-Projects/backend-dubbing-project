using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.FileSystem
{
    public class FilesRepository : IFileRepository
    {
        private const string _audiosFolder = "Audio Files";
        private readonly string _audioFilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), _audiosFolder);

        public async Task UploadAsync(Audio audio)
        {
            string path = Path.Combine(_audioFilesFolderPath, audio.FileName);

            await File.WriteAllBytesAsync(path, audio.AudioFile);
        }

        public void Delete(IEnumerable<Audio> audios)
        {
            foreach(Audio audio in audios)
            {
                string path = Path.Combine(_audioFilesFolderPath, audio.FileName);

                File.Delete(path);
            }
        }
    }
}