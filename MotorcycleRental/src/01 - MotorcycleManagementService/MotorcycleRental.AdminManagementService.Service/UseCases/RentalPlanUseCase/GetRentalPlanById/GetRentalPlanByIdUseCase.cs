
namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetRentalPlanById
{
    public class GetRentalPlanByIdUseCase : IGetRentalPlanByIdUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IMapper _mapper;

        public GetRentalPlanByIdUseCase(IRentalPlanRepository rentalPlanRepository, IMapper mapper)
        {
            _rentalPlanRepository = rentalPlanRepository;
            _mapper = mapper;
        }

        public async Task<RentalPlanInputOutput> Execute(Guid Id)
        {
            RentalPlan rentalPlan = await _rentalPlanRepository.GetByIdAsync(Id);
            RentalPlanInputOutput output = _mapper.Map<RentalPlanInputOutput>(rentalPlan);
            return output;
        }
    }
}
