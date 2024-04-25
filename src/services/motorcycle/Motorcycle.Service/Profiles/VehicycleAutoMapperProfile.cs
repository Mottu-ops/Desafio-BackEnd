using AutoMapper;
using Motorcycle.Domain.Entities;
using Motorcycle.Service.DTO;

namespace Motorcycle.Service.Profiles;
public class VehicleAutoMapperProfile : Profile {
    public VehicleAutoMapperProfile() {
        CreateMap<VehicleDto, Vehicle>();
        CreateMap<Vehicle, VehicleDto>();
    }
}