namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.AddRentalPlan
{
    public class AddRentalPlanUseCase : IAddRentalPlanUseCase
    {
        private readonly IMapper _mapper;
        private readonly IRentalPlanRepository _rentalPlanRepository;

        public AddRentalPlanUseCase(IMapper mapper, IRentalPlanRepository rentalPlanRepository)
        {
            _mapper = mapper;
            _rentalPlanRepository = rentalPlanRepository;
        }

        public async Task<AddRentalPlanOutput> Execute(AddRentalPlanInput input)
        {
            var rentalPlan = _mapper.Map<RentalPlan>(input);

            rentalPlan.SetCreatedAtDate();
            await _rentalPlanRepository.AddAsync(rentalPlan);
            
            return _mapper.Map<AddRentalPlanOutput>(rentalPlan);
        }
    }
}
