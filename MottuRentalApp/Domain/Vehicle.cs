using System.Text.RegularExpressions;

namespace MottuRentalApp.Domain
{
  public class Vehicle
  {
    public Vehicle(string licensePlate, int? year, string? model)
    {
      licensePlate = Regex.Replace(licensePlate, "[^A-Za-z0-9]+", string.Empty);
      LicensePlate = licensePlate;
      Year = year < 1900 || year > DateTime.UtcNow.Year ? throw new ArgumentException("INVALID_YEAR") : year;
      Model = model;
    }

    public string LicensePlate { get; set; }
    public int? Year { get; set; }
    public string? Model { get; set; }
  }
}
