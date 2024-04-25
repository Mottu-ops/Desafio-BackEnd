using AutoMapper;
using Plan.Domain.Entity;
using Plan.Service.DTO;

namespace Plan.Service.Profiles;
public class PlanAutoMapperProfile : Profile {
    public PlanAutoMapperProfile() {
        CreateMap<PlanDto, RentPlan>();
        CreateMap<RentPlan, PlanDto>();
    }
}