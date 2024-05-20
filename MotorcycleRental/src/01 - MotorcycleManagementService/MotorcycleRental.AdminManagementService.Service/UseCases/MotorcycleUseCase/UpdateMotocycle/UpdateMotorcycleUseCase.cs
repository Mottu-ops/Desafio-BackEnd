namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.UpdateMotocycle
{
    public class UpdateMotorcycleUseCase : IUpdateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public UpdateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        public async Task<MotorcycleInputOutput> Execute(MotorcycleInputOutput input)
        {
            Motorcycle motorcycle = _mapper.Map<Motorcycle>(input);
            motorcycle.SetUpdateDate();

            await _motorcycleRepository.UpdateAsync(motorcycle);

            MotorcycleInputOutput output = _mapper.Map<MotorcycleInputOutput>(motorcycle);

            return output;
        }
    }
}
