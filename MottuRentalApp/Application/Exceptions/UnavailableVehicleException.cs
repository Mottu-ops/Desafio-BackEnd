using MottuRentalApp.Domain;

namespace MottuRentalApp.Application.Exceptions
{
  public class UnavailableVehicleException : InvalidOperationException
  {
    public UnavailableVehicleException(string message, string thrownAt) : base(message)
    {
      ThrownAt = thrownAt;
    }

    public string ThrownAt { get; }
  }
}