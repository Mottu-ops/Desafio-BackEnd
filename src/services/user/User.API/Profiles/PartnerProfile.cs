using AutoMapper;
using User.API.ViewModels;
using User.Service.DTO;

namespace User.API.Profiles;
 
public class PartnerProfile : Profile {
    public PartnerProfile() {
        CreateMap<CreateUserViewModel, PartnerDto>();
        CreateMap<UpdateUserViewModel, PartnerDto>();
    }
}