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
            CreateMap<Job, JobViewModel>()
                .ForMember(x => x.UserName, dest => dest.MapFrom(x => GetName(x.User))).ReverseMap();
            CreateMap<UpdateJobModel, Job>().ForMember(x => x.Id, dest => dest.Ignore());
            CreateMap<Job, CreateJobViewModel>().ReverseMap();
        }

        private string GetName(User user) => user?.Name;
    }
}