using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Facades
{
  public interface IRentalVehiclesFacade
  {
    public bool IsVehicleAvailable(string licensePlate);
    public Rental RentAvailableVehicle(string userId, string endTerm);
  }
}