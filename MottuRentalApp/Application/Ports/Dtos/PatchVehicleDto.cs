namespace MottuRentalApp.Application.Ports
{
  public struct PatchVehicleDto
  {
    public PatchVehicleDto(string? licensePlate, int? year, string? model)
    {
      LicensePlate = licensePlate;
      Year = year;
      Model = model;
    }

    public string? LicensePlate { get; }
    public int? Year { get; }
    public string? Model { get; }
  }
}