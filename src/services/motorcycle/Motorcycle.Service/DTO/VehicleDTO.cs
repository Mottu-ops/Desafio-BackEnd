namespace Motorcycle.Service.DTO;

public class VehicleDto
{
    public long Id { get; set; }
    public string PlateCode { get; set; }
    public string Color { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
    public long Owner { get; set; }

    public VehicleDto() { }
    public VehicleDto(long id, string plateCode, string color, string model, string year, long owner)
    {
        Id = id;
        PlateCode = plateCode;
        Color = color;
        Model = model;
        Year = year;
        Owner = owner;
    }
}