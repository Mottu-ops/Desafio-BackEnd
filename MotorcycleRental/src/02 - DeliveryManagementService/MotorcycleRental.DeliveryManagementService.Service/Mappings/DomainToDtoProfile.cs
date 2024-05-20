using AutoMapper;
using MotorcycleRental.DeliveryManagementService.Service.DTOs;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.DeliveryManagementService.Service.Mappings
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            //Deliveryman
            CreateMap<DeliverymanAddDto, Deliveryman>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.DriverLicenseNumber, opt => opt.MapFrom(src => src.DriverLicenseNumber))
                .ForMember(dest => dest.DriverLicenseType, opt => opt.MapFrom(src => src.DriverLicenseType));

            CreateMap<DeliverymanFullDto, Deliveryman>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.DriverLicenseNumber, opt => opt.MapFrom(src => src.DriverLicenseNumber))
                .ForMember(dest => dest.DriverLicenseType, opt => opt.MapFrom(src => src.DriverLicenseType))
                .ForMember(dest => dest.CNHImageUrl, opt => opt.MapFrom(src => src.CNHImageUrl))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived))
                .ReverseMap();

            //RentalContract
            CreateMap<RentalContractAddDto, RentalContract>()
                .ForMember(dest => dest.DeliverymanId, opt => opt.MapFrom(src => src.DeliverymanId))
                .ForMember(dest => dest.RentanPlanId, opt => opt.MapFrom(src => src.RentanPlanId))
                .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.MotorcycleId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));
            //.ForMember(dest => dest.ExpectedEndDate, opt => opt.MapFrom(src => src.ExpectedEndDate));

            CreateMap<RentalContractFullDto, RentalContract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeliverymanId, opt => opt.MapFrom(src => src.DeliverymanId))
                .ForMember(dest => dest.RentanPlanId, opt => opt.MapFrom(src => src.RentanPlanId))
                .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.MotorcycleId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ExpectedEndDate, opt => opt.MapFrom(src => src.ExpectedEndDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.WasReturned, opt => opt.MapFrom(src => src.WasReturned))
                .ForMember(dest => dest.RentalValue, opt => opt.MapFrom(src => src.RentalValue))
                .ForMember(dest => dest.AdditionalFineValue, opt => opt.MapFrom(src => src.AdditionalFineValue))
                .ForMember(dest => dest.AdditionalDailyValue, opt => opt.MapFrom(src => src.AdditionalDailyValue))
                .ForMember(dest => dest.TotalRentalValue, opt => opt.MapFrom(src => src.TotalRentalValue))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived))
                .ReverseMap();

        }
    }
}
