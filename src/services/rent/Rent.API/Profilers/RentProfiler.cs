using AutoMapper;
using Rent.API.ViewModels;
using Rent.Service.DTO;

namespace Rent.API.Profilers;

public class RentProfiler : Profile {
    public RentProfiler() {
        CreateMap<RentCreationViewModel, TransactionDTO>();
        CreateMap<RentUpdatingViewModel, TransactionDTO>();
    }
}