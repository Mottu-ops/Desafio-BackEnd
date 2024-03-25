using MottuRentalApp.Application.Exceptions;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.UseCases
{
  public class PatchVehicleUseCase
  {
    private readonly IVehiclesPort _vehiclesPort;
    private const string FEATURE_NAME = "ADD_VEHICLE";

    public PatchVehicleUseCase(IVehiclesPort vehiclesPort)
    {
      this._vehiclesPort = vehiclesPort;
    }

    public Vehicle execute(PatchVehicleDto dto)
    {
      try
      {
        this.validateData(dto);

        return this._vehiclesPort.patchVehicle(dto);
      }
      catch (Exception exc)
      {
        throw new InvalidVehicleException(exc.Message, FEATURE_NAME);
      }
    }

    private void validateData(PatchVehicleDto dto)
    {
      if (string.IsNullOrEmpty(dto.LicensePlate) || string.IsNullOrWhiteSpace(dto.LicensePlate)) {
        throw new InvalidVehicleException("Invalid data to be patched.", FEATURE_NAME);
      }
    }
  }
}
