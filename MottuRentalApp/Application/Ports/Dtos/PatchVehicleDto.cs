namespace MottuRentalApp.Application.Ports
{
  public struct PatchVehicleDto
  {
    public PatchVehicleDto(string identifier, string? licensePlate, int? year, string? model)
    {
      Identifier = identifier;
      LicensePlate = licensePlate;
      Year = year;
      Model = model;
    }

    public string Identifier { get; }
    public string? LicensePlate { get; }
    public int? Year { get; }
    public string? Model { get; }
  }
}