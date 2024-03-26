using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Facades
{
  public class RentalVehiclesFacade
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
        var rental = this._rentalsPort.FindByVehicleId(vehicle.Identifier);

        return rental == null || rental.Status == RentalStatus.DONE;
      } else {
        return false;
      }
    }
  }
}
