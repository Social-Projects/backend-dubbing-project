using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using Web.ViewModels;

namespace Web.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Performance, PerformanceViewModel>();
            CreateMap<PerformanceViewModel, Performance>();

            CreateMap<Speech, SpeechViewModel>();
            CreateMap<SpeechViewModel, Speech>();

            CreateMap<Language, LanguageViewModel>();
            CreateMap<LanguageViewModel, Language>();

            CreateMap<Audio, AudioFileViewModel>();
            CreateMap<AudioFileViewModel, Audio>()
                .ForMember("FileName", cnf => cnf.MapFrom(m => m.File.FileName));
            CreateMap<Audio, AudioViewModel>();
            CreateMap<AudioViewModel, Audio>();
        }
    }
}
