using AutoMapper;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriver;
using MT.Backend.Challenge.Application.Commands.DeliveryDrivers.AddDeliveryDriverLicense;
using MT.Backend.Challenge.Application.Commands.Motorcycles.AddMotorcycle;
using MT.Backend.Challenge.Application.Commands.Motorcycles.ChangeMotorcyclePlate;
using MT.Backend.Challenge.Application.Commands.Rentals.AddRental;
using MT.Backend.Challenge.Application.Commands.Rentals.ChangeRental;
using MT.Backend.Challenge.Domain.Entities;

namespace MT.Backend.Challenge.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DeliveryDriver, AddDeliveryDriverRequest>().ReverseMap();
            CreateMap<DeliveryDriver, AddDeliveryDriverLicenseRequest>().ReverseMap();

            CreateMap<Motorcycle, AddMotorcycleRequest>().ReverseMap();
            CreateMap<Motorcycle, ChangeMotorcyclePlateRequest>().ReverseMap();

            CreateMap<Rental, ChangeRentalRequest>().ReverseMap();
            CreateMap<Rental, AddRentalRequest>().ReverseMap();
        }
    }
}
