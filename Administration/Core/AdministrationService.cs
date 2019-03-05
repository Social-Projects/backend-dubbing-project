using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core
{
    internal class AdministrationService : IAdministrationService
    {
        private readonly IPerformanceService _performanceService;
        private readonly ISpeechService _speechService;
        private readonly IAudioService _audioService;
        private readonly ILanguageService _languageService;

        public AdministrationService(
            IPerformanceService performanceService,
            ISpeechService speechService,
            IAudioService audioService,
            ILanguageService languageService)
        {
            _performanceService = performanceService;
            _speechService = speechService;
            _audioService = audioService;
            _languageService = languageService;
        }

        public async Task<List<Performance>> GetAllPerformancesAsync()
        {
            return await _performanceService.GetAllAsync();
        }

        public async Task<List<Speech>> GetAllSpeechesAsync()
        {
            return await _speechService.GetAllAsync();
        }

        public async Task<List<Audio>> GetAllAudiosAsync()
        {
            return await _audioService.GetAllAsync();
        }

        public async Task<List<Language>> GetAllLanguagesAsync()
        {
            return await _languageService.GetAllAsync();
        }

        public async Task<Performance> GetPerformanceByIdAsync(int id)
        {
            return await _performanceService.GetByIdAsync(id);
        }

        public async Task<Speech> GetSpeechByIdAsync(int id)
        {
            return await _speechService.GetByIdAsync(id);
        }

        public async Task<Audio> GetAudioByIdAsync(int id)
        {
            return await _audioService.GetByIdAsync(id);
        }

        public async Task<Language> GetLanguageByIdAsync(int id)
        {
            return await _languageService.GetByIdAsync(id);
        }

        public async Task<List<Speech>> GetSpeechesAsync(int id)
        {
            return await _performanceService.GetChildrenByIdAsync(id);
        }

        public async Task CreatePerformanceAsync(Performance performance)
        {
            await _performanceService.CreateAsync(performance);
        }

        public async Task CreateSpeechAsync(Speech speech)
        {
            await _speechService.CreateAsync(speech);
        }

        public async Task CreateAudioAsync(Audio audio)
        {
            await _audioService.CreateAsync(audio);
        }

        public async Task CreateLanguageAsync(Language language)
        {
            await _languageService.CreateAsync(language);
        }

        public async Task UpdatePerformanceAsync(int id, Performance performance)
        {
            await _performanceService.UpdateAsync(id, performance);
        }

        public async Task UpdateSpeechAsync(int id, Speech speech)
        {
            await _speechService.UpdateAsync(id, speech);
        }

        public async Task UpdateAudioAsync(int id, Audio audio)
        {
            await _audioService.UpdateAsync(id, audio);
        }

        public async Task UpdateLanguageAsync(int id, Language language)
        {
            await _languageService.UpdateAsync(id, language);
        }

        public async Task DeletePerformanceAsync(int id)
        {
            await _performanceService.DeleteAsync(id);
        }

        public async Task DeleteSpeechAsync(int id)
        {
            await _speechService.DeleteAsync(id);
        }

        public async Task DeleteLanguageAsync(int id)
        {
            await _languageService.DeleteAsync(id);
        }

        public async Task UploadAudioAsync(Audio audio)
        {
            await _audioService.UploadAsync(audio);
        }
    }
}