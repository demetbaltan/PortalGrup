using AutoMapper;
using Entities.Concrete.ApplicationClasses;
using Entities.Dtos.ApplicationDtos;

namespace WebApp.AutoMapper.ApplicationClasses
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationDto>();
            CreateMap<ApplicationDto, Application>();
        }
    }
}
