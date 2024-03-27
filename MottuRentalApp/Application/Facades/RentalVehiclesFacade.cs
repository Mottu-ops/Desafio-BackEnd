using MongoDB.Driver.Linq;
using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Facades
{
  public class RentalVehiclesFacade : IRentalVehiclesFacade
  {
    private readonly IVehiclesPort _vehiclesPort;
    private readonly IRentalsPort _rentalsPort;

    public RentalVehiclesFacade(IVehiclesPort vehiclesPort, IRentalsPort rentalsPort)
    {
      this._vehiclesPort = vehiclesPort;
      this._rentalsPort = rentalsPort;
    }

    public bool IsVehicleAvailable(string licensePlate)
    {
      var vehicle = this._vehiclesPort.FindVehicleByPlate(licensePlate);

      if (vehicle != null) {
        var rental = this._rentalsPort.FindByVehiclePlate(vehicle.LicensePlate);

        return rental == null || rental.Status == RentalStatus.DONE;
      } else {
        return false;
      }
    }

    public Rental RentAvailableVehicle(string userId, string endTerm)
    {
      var ongoingRentals = this._rentalsPort.FetchOngoing();
      var vehicles = this._vehiclesPort.FetchVehiclesExcept(ongoingRentals.Select((o) => o.VehicleId).ToList());
      if (vehicles.Count < 1) {
        throw new Exception("NO_VEHICLES_AVAILABLE");
      }

      return new Rental(userId, vehicles.FirstOrDefault()?.LicensePlate, endTerm);
    }
  }
}
