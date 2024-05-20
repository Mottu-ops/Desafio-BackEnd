namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class RentalContractAddDto
    {
        public Guid DeliverymanId { get; set; }
        public Guid RentanPlanId { get; set; }
        public Guid MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
