using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using Web.ViewModels;

namespace Web.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Performance, PerformanceViewModel>()
                .ForMember("Speeches", conf => conf.Ignore());
            CreateMap<PerformanceViewModel, Performance>();

            CreateMap<Speech, SpeechViewModel>();
        }
    }
}
