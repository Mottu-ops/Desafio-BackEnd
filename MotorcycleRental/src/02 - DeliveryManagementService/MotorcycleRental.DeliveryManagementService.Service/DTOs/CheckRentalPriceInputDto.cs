namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class CheckRentalPriceInputDto
    {
        public Guid ContractId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
