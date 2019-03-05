using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core
{
    public interface IAdministrationService
    {
        Task<List<Performance>> GetAllPerformancesAsync();

        Task<List<Speech>> GetAllSpeechesAsync();

        Task<List<Audio>> GetAllAudiosAsync();

        Task<List<Language>> GetAllLanguagesAsync();

        Task<Performance> GetPerformanceByIdAsync(int id);

        Task<Speech> GetSpeechByIdAsync(int id);

        Task<Audio> GetAudioByIdAsync(int id);

        Task<Language> GetLanguageByIdAsync(int id);

        Task<List<Speech>> GetSpeechesAsync(int id);

        Task CreatePerformanceAsync(Performance performance);

        Task CreateSpeechAsync(Speech speech);

        Task CreateAudioAsync(Audio audio);

        Task CreateLanguageAsync(Language language);

        Task UpdatePerformanceAsync(int id, Performance performance);

        Task UpdateSpeechAsync(int id, Speech speech);

        Task UpdateAudioAsync(int id, Audio audio);

        Task UpdateLanguageAsync(int id, Language language);

        Task DeletePerformanceAsync(int id);

        Task DeleteSpeechAsync(int id);

        Task DeleteLanguageAsync(int id);

        Task UploadAudioAsync(Audio audio);
    }
}