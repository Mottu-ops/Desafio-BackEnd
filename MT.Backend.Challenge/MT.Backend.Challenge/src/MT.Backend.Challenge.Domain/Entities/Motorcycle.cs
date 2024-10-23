
namespace MT.Backend.Challenge.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string LicensePlate { get; set; } = null!;
        public string? Brand { get; set; }
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string? Color { get; set; } 
    }
}

