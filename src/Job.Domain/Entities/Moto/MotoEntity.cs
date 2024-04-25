namespace Job.Domain.Entities.Moto;

public sealed class MotoEntity(int year, string model, string plate) : BaseEntity
{

    public int Year { get; private set; } = year;
    public string Model { get; private set; } = model;
    public string Plate { get; private set; } = plate;

    public void Update(int year, string model, string plate)
    {
        base.Update();
        Year = year;
        Model = model;
        Plate = plate;
    }
}