using AutoMapper;
using User.API.ViewModels;
using User.Domain.Entities;

namespace User.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, Client>();
        }
    }
}
