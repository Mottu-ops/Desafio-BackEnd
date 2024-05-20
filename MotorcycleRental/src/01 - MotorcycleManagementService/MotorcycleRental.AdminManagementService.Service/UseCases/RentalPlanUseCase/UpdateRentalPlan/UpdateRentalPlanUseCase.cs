
namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.UpdateRentalPlan
{
    public class UpdateRentalPlanUseCase : IUpdateRentalPlanUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IMapper _mapper;

        public UpdateRentalPlanUseCase(IRentalPlanRepository rentalPlanRepository, IMapper mapper)
        {
            _rentalPlanRepository = rentalPlanRepository;
            _mapper = mapper;
        }

        public async Task<RentalPlanInputOutput> Execute(UpdateRentalPlanIntput input)
        {
            //TODO: Validar se a moto tá disponivel e não possui registro de locação

            RentalPlan rentalPlan = _mapper.Map<RentalPlan>(input);
            rentalPlan.SetUpdateDate();

            await _rentalPlanRepository.UpdateAsync(rentalPlan);

            RentalPlanInputOutput output = _mapper.Map<RentalPlanInputOutput>(rentalPlan);

            return output;
        }
    }
}
