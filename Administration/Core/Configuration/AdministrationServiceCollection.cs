using Microsoft.Extensions.DependencyInjection;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Configuration
{
    public class AdministrationServiceCollection : IAdministrationServiceCollection
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IAdministrationService, AdministrationService>();

            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPerformanceService, PerformanceService>();
            services.AddScoped<ISpeechService, SpeechService>();
        }
    }
}