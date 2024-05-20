using MotorcycleRental.AdminManagementService.Service.Exceptions;
using MotorcycleRental.Infraestructure.MessageBus;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.AddMotorcycle
{
    public class AddMotorcycleUseCase : IAddMotorcycleUseCase
    {
        private readonly IMapper _mapper;
        private readonly IEventProcessor _eventProcessor;
        private readonly IMotorcycleRepository _motorcycleRepository;
        public AddMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IMapper mapper, IEventProcessor eventProcessor)
        {
            _mapper = mapper;
            _eventProcessor = eventProcessor;
            _motorcycleRepository = motorcycleRepository;
        }
        public async Task<AddMotorcycleOutput> Execute(AddMotorcycleInput input)
        {

            Motorcycle motorcycle = await _motorcycleRepository.GetByPlateAsync(input.Plate);
            if (motorcycle != null)
                throw new DuplicateKeyException("Plate informed, has already been registered!");

            motorcycle = _mapper.Map<Motorcycle>(input);

            motorcycle.SetCreatedAtDate();
            await _motorcycleRepository.AddAsync(motorcycle);

            if (motorcycle.Year == 2024)
                _eventProcessor.Process(motorcycle.Events);

            return new AddMotorcycleOutput(motorcycle.Id,
                                           motorcycle.Year,
                                           motorcycle.Model,
                                           motorcycle.Plate,
                                           motorcycle.CreatedAt,
                                           motorcycle.IsActived);
        }
    }
}
