namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class RentalContractFullDto
    {
        public Guid Id { get; set; }
        public Guid DeliverymanId { get; set; }
        public Guid RentanPlanId { get; set; }
        public Guid MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool WasReturned { get; set; }
        public decimal RentalValue { get; set; }
        public decimal AdditionalFineValue { get; set; }
        public decimal AdditionalDailyValue { get; set; }
        public decimal TotalRentalValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActived { get; set; }
    }
}
