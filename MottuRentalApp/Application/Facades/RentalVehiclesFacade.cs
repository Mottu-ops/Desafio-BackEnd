using MottuRentalApp.Application.Ports;

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
  }
}
