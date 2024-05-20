namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase.AddMotorcycle
{
    public class AddMotorcycleOutput
    {
        public AddMotorcycleOutput(Guid id, int year, string model, string plate, DateTime createdAt, bool isActived)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate;
            CreatedAt = createdAt;
            IsActived = isActived;
        }

        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public bool IsActived { get; protected set; }
    }
}
