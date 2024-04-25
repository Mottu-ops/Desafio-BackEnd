using AutoMapper;
using Plan.API.ViewModels;
using Plan.Service.DTO;


namespace Plan.API.Profiles;
 
public class PlanProfile : Profile {
    public PlanProfile() {
        CreateMap<CreatePlanViewModel, PlanDto>();
        CreateMap<UpdatePlanViewModel, PlanDto>();
    }
}