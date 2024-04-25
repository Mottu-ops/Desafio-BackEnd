using AutoMapper;
using Motorcycle.API.ViewModels;
using Motorcycle.Service.DTO;

namespace Motorcycle.API.Profiles;
 
public class VehicleProfile : Profile {
    public VehicleProfile() {
        CreateMap<CreateVehicleViewModel, VehicleDto>();
        CreateMap<UpdateVehicleViewModel, VehicleDto>();
    }
}