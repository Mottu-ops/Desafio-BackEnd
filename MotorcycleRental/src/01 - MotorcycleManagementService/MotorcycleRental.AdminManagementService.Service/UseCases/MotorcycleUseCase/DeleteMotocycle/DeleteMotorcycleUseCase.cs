using MotorcycleRental.AdminManagementService.Service.Exceptions;

namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.DeleteMotocycle
{
    public class DeleteMotorcycleUseCase : IDeleteMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public DeleteMotorcycleUseCase(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task Execute(DeleteMotorcycleInput input)
        {
            Motorcycle motorcycle = await _motorcycleRepository.GetByIdAsync(input.Id);

            if (motorcycle == null)
                throw new NotFoundException("This mentioned plate does not exist");

            await _motorcycleRepository.DeleteAsync(motorcycle);
        }
    }
}
