using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Exceptions
{
  public class InvalidVehicleException : InvalidOperationException
  {
    public InvalidVehicleException(Vehicle vehicle, string thrownAt) :
      base($"{thrownAt} could not process: {vehicle.LicensePlate}-{vehicle.LicensePlate}-{vehicle.Year}-{vehicle.Model}")
    {
      ThrownAt = thrownAt;
    }

    public InvalidVehicleException(string message, string thrownAt) : base(message)
    {
      ThrownAt = thrownAt;
    }

    public string ThrownAt { get; }
  }
}