namespace MottuRentalApp.Application.Exceptions
{
  public class InvalidPatchException : InvalidOperationException
  {
    public InvalidPatchException(string message, string thrownAt) : base(message)
    {
      ThrownAt = thrownAt;
    }

    public string ThrownAt { get; }
  }
}