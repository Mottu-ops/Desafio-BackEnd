using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;
using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Facades;

namespace MottuRentalApp.Application.UseCases
{
  public class RentVehicleUseCase
  {
    private readonly IRentalsPort _rentalsPort;
    private readonly IRentalVehiclesFacade _rentalVehiclesFacade;
    private const string FEATURE_NAME = "RENT_VEHICLE";
    public RentVehicleUseCase(IRentalsPort rentalsPort, IRentalVehiclesFacade rentalVehiclesFacade)
    {
      this._rentalsPort = rentalsPort;
      this._rentalVehiclesFacade = rentalVehiclesFacade;
    }

    public void Execute(string userId, string endTerm)
    {
      CheckUserAvailability(userId);

      this._rentalVehiclesFacade.RentAvailableVehicle(userId, endTerm);
    }

    private void CheckUserAvailability(string userId)
    {
      var rental = this._rentalsPort.FindByUser(userId);
      if (rental != null && (rental?.Status == RentalStatus.ACTIVE || rental?.Status == RentalStatus.PENDING)) {
        throw new InvalidRentException("USER_ALREADY_ON_RENT", FEATURE_NAME);
      }
    }
  }
}
