using AutoMapper;
using User.Domain.Entities;
using User.Service.DTO;

namespace User.Service.Profiles;
public class UserAutoMapperProfile : Profile
{
    public UserAutoMapperProfile()
    {
        CreateMap<PartnerDto, Partner>();
        CreateMap<Partner, PartnerDto>();
    }
}