using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.UseCases
{
  public class FindVehicleUseCase
  {
    private readonly IVehiclesPort _vehiclesPort;
    private const string FEATURE_NAME = "FIND_VEHICLE";

    public FindVehicleUseCase(IVehiclesPort vehiclesPort)
    {
      this._vehiclesPort = vehiclesPort;
    }

    public Vehicle? Execute(string plate)
    {
      if (string.IsNullOrEmpty(plate) || string.IsNullOrWhiteSpace(plate)) {
        throw new InvalidVehicleException("Invalid plate", FEATURE_NAME);
      }

      return this._vehiclesPort.FindVehicleByPlate(plate);
    }
  }
}
