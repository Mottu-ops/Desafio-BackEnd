using AutoMapper;
using User.Domain.Entities;
using User.Service.DTO;

namespace Order.Tests.Service.Configurations;

public static class AutoMapperConfiguration
{
    public static IMapper GetConfig()
    {
        var autoMapperConfig = new MapperConfiguration(
            config => {
                config.CreateMap<PartnerDto, Partner>();
                config.CreateMap<Partner, PartnerDto>();
                }
            );
        return autoMapperConfig.CreateMapper();
    }
}