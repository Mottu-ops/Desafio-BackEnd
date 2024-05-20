namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.GetMotorcycleByPlate
{
    public class GetMotorcycleByPlateUseCase : IGetMotorcycleByPlateUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public GetMotorcycleByPlateUseCase(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        public async Task<MotorcycleInputOutput> Execute(GetMotorcycleByPlateInput input)
        {
            Motorcycle motorcycle = await _motorcycleRepository.GetByPlateAsync(input.Plate);
            MotorcycleInputOutput motorcycleOutput = _mapper.Map<MotorcycleInputOutput>(motorcycle);
            return motorcycleOutput;
        }
    }
}
