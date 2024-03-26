using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.UseCases
{
  public class AddVehicleUseCase
  {
    private readonly IVehiclesPort _vehiclesPort;
    private const string FEATURE_NAME = "ADD_VEHICLE";

    public AddVehicleUseCase(IVehiclesPort vehiclesPort)
    {
      this._vehiclesPort = vehiclesPort;
    }

    public Vehicle Execute(Vehicle vehicle)
    {
      try
      {
        var found = this._vehiclesPort.FindVehicleByPlate(licensePlate: vehicle.LicensePlate);
        if (found != null) {
          throw new InvalidVehicleException($"{found.LicensePlate} already exists.", FEATURE_NAME);
        }

        return this._vehiclesPort.SaveVehicle(vehicle);
      }
      catch (Exception)
      {
        throw new InvalidVehicleException(vehicle, FEATURE_NAME);
      }
    }
  }
}
