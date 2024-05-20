
namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.GetAllRentalPlan
{
    public class GetAllRentalPlanUseCase : IGetAllRentalPlanUseCase
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IMapper _mapper;

        public GetAllRentalPlanUseCase(IRentalPlanRepository rentalPlanRepository, IMapper mapper)
        {
            _rentalPlanRepository = rentalPlanRepository;
            _mapper = mapper;
        }

        public async Task<List<RentalPlanInputOutput>> Execute()
        {
            IEnumerable<RentalPlan> rentalPlan = await _rentalPlanRepository.GetAllAsync();

            if (rentalPlan == null)
                return new List<RentalPlanInputOutput>();

            List<RentalPlanInputOutput> list = _mapper.Map<List<RentalPlanInputOutput>>(rentalPlan);

            return list;
        }
    }
}
