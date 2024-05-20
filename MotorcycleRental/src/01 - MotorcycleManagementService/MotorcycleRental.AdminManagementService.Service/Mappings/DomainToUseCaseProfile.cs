using MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan;

namespace MotorcycleRental.AdminManagementService.Service.Mappings
{
    public class DomainToUseCaseProfile : Profile
    {
        public DomainToUseCaseProfile()
        {
            //Motorcycle
            CreateMap<AddMotorcycleInput, Motorcycle>()                
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Plate));

            CreateMap<Motorcycle, MotorcycleInputOutput>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Plate))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived)).ReverseMap();

            //RentalPlan
            CreateMap<AddRentalPlanInput, RentalPlan>()
            .ForMember(dest => dest.Descrition, opt => opt.MapFrom(src => src.Descrition))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
            .ForMember(dest => dest.DayValue, opt => opt.MapFrom(src => src.DayValue))
            .ForMember(dest => dest.PercentageFine, opt => opt.MapFrom(src => src.PercentageFine))
            .ForMember(dest => dest.AdditionalValueDaily, opt => opt.MapFrom(src => src.AdditionalValueDaily));

            CreateMap<RentalPlan, AddRentalPlanOutput>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Descrition, opt => opt.MapFrom(src => src.Descrition))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
            .ForMember(dest => dest.DayValue, opt => opt.MapFrom(src => src.DayValue))
            .ForMember(dest => dest.PercentageFine, opt => opt.MapFrom(src => src.PercentageFine))
            .ForMember(dest => dest.AdditionalValueDaily, opt => opt.MapFrom(src => src.AdditionalValueDaily))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived));

            CreateMap<RentalPlan, RentalPlanInputOutput>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Descrition, opt => opt.MapFrom(src => src.Descrition))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
            .ForMember(dest => dest.DayValue, opt => opt.MapFrom(src => src.DayValue))
            .ForMember(dest => dest.PercentageFine, opt => opt.MapFrom(src => src.PercentageFine))
            .ForMember(dest => dest.AdditionalValueDaily, opt => opt.MapFrom(src => src.AdditionalValueDaily))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsActived)).ReverseMap();

            CreateMap<UpdateRentalPlanIntput, RentalPlan>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Descrition, opt => opt.MapFrom(src => src.Descrition))
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
            .ForMember(dest => dest.DayValue, opt => opt.MapFrom(src => src.DayValue))
            .ForMember(dest => dest.PercentageFine, opt => opt.MapFrom(src => src.PercentageFine))
            .ForMember(dest => dest.AdditionalValueDaily, opt => opt.MapFrom(src => src.AdditionalValueDaily));
        }
    }
}
