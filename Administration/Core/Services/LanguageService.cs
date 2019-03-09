using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    public class LanguageService : GenericService<Language>, ILanguageService
    {
        public IAudioService _audioService;

        public LanguageService(IRepository<Language> repository, IAudioService audioService)
            : base(repository)
        {
            _audioService = audioService;
        }

        public override async Task DeleteAsync(int id)
        {
            var langWithAudios = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            if (langWithAudios == null)
                return;
            var audios = langWithAudios.Audios;

            await _audioService.DeleteFileAsync(audios);

            var auidiosId = new List<int>();
            foreach (var audio in audios)
            {
                auidiosId.Add(audio.Id);
            }

            foreach (var audioId in auidiosId)
            {
                await _audioService.DeleteAsync(audioId);
            }

            await Repository.DeleteAsync(langWithAudios);
        }
    }
}