using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Dtos;
using SoftServe.ITAcademy.BackendDubbingProject.Models;

namespace SoftServe.ITAcademy.BackendDubbingProject.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();
        }
    }
}