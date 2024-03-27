namespace MottuRentalApp.Application.Exceptions
{
  public class InvalidRentException : InvalidOperationException
  {
    public InvalidRentException(string message, string thrownAt) : base(message)
    {
      ThrownAt = thrownAt;
    }

    public string ThrownAt { get; }
  }
}