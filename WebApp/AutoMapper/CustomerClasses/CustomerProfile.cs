using AutoMapper;
using Entities.Concrete.CustomerClasses;
using Entities.Dtos.CustomerDtos;

namespace WebApp.AutoMapper.CustomerClasses
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
