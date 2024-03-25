using System.Text.RegularExpressions;

namespace MottuRentalApp.Domain
{
  public class Vehicle
  {
    public Vehicle(string licensePlate, int year, string model)
    {
      Identifier = Guid.NewGuid().ToString();
      licensePlate = Regex.Replace(licensePlate, "[^A-Za-z0-9]+", string.Empty);
      LicensePlate = licensePlate;
      Year = year < 1900 || year > DateTime.UtcNow.Year ? throw new ArgumentException("INVALID_YEAR") : year;
      Model = model;
    }

    public string Identifier { get; }
    public string LicensePlate { get; }
    public int Year { get; }
    public string Model { get; }
  }
}
