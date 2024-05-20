namespace MotorcycleRental.AdminManagementService.Service.UseCases.MotorcycleUseCase
{
    public class MotorcycleInputOutput
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActived { get; set; }
    }
}
