using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core
{
    public interface IAdministrationService
    {
        Task<List<PerformanceDTO>> GetAllPerformancesAsync();

        Task<List<SpeechDTO>> GetAllSpeechesAsync();

        Task<List<AudioDTO>> GetAllAudiosAsync();

        Task<List<LanguageDTO>> GetAllLanguagesAsync();

        Task<PerformanceDTO> GetPerformanceByIdAsync(int id);

        Task<SpeechDTO> GetSpeechByIdAsync(int id);

        Task<AudioDTO> GetAudioByIdAsync(int id);

        Task<LanguageDTO> GetLanguageByIdAsync(int id);

        Task<List<SpeechDTO>> GetSpeechesAsync(int id);

        Task<List<AudioDTO>> GetAudiosAsync(int id);

        Task CreatePerformanceAsync(PerformanceDTO performanceDTO);

        Task CreateSpeechAsync(SpeechDTO speechDTO);

        Task CreateAudioAsync(AudioDTO audioDTO);

        Task CreateLanguageAsync(LanguageDTO languageDTO);

        Task UpdatePerformanceAsync(int id, PerformanceDTO performanceDTO);

        Task UpdateSpeechAsync(int id, SpeechDTO speechDTO);

        Task UpdateAudioAsync(int id, AudioDTO audioDTO);

        Task UpdateLanguageAsync(int id, LanguageDTO languageDTO);

        Task DeletePerformanceAsync(int id);

        Task DeleteSpeechAsync(int id);

        Task DeleteLanguageAsync(int id);

        Task UploadAudioAsync(AudioFileDTO audioFileDTO);

        Task DeleteAudio(int id);

        void DeleteAudioFiles(IEnumerable<string> fileNames);

        Task UploadWaitingAudioAsync(AudioFileDTO audioFileDTO);
    }
}