
namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetAllMotorcycle
{
    public class GetAllMotorcycleUseCase : IGetAllMotorcycleUseCase
    {

        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public GetAllMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        public async Task<List<MotorcycleInputOutput>> Execute()
        {
            IEnumerable<Motorcycle> motorcycle = await _motorcycleRepository.GetAllAsync();

            if (motorcycle == null)
                return new List<MotorcycleInputOutput>();

            List<MotorcycleInputOutput> list = _mapper.Map<List<MotorcycleInputOutput>>(motorcycle);

            return list;
        }
    }
}
