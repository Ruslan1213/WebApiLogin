using AutoMapper;
using UserManagment.Domain.Models;
using UserManagment.Models;

namespace UserManagment.Configuration
{
    public class MappingConfigurator : Profile
    {
        public MappingConfigurator()
        {
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<RegisterViewModel, LoginViewModel>().ReverseMap();
        }
    }
}