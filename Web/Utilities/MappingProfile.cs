using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Web.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Performance, PerformanceDTO>();
            CreateMap<PerformanceDTO, Performance>();

            CreateMap<Speech, SpeechDTO>();
            CreateMap<SpeechDTO, Speech>();

            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();

            CreateMap<Audio, AudioFileDTO>();
            CreateMap<AudioFileDTO, Audio>()
                .ForMember("FileName", cnf => cnf.MapFrom(m => m.File.FileName));

            CreateMap<Audio, AudioDTO>();
            CreateMap<AudioDTO, Audio>();
        }
    }
}
