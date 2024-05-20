namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class CheckTotalRentalPriceDto
    {
        public CheckTotalRentalPriceDto(Guid contractId, string descriptionPlan, decimal dayValue, decimal rentalValue, decimal additionalFineValue, decimal additionalDailyValue, decimal totalRentalValue)
        {
            ContractId = contractId;
            DescriptionPlan = descriptionPlan;
            DayValue = dayValue;
            RentalValue = rentalValue;
            AdditionalFineValue = additionalFineValue;
            AdditionalDailyValue = additionalDailyValue;
            TotalRentalValue = totalRentalValue;
        }

        public Guid ContractId { get; set; }
        public string DescriptionPlan { get; set; }
        public decimal DayValue { get; private set; }
        public decimal RentalValue { get; private set; }
        public decimal AdditionalFineValue { get; private set; }
        public decimal AdditionalDailyValue { get; private set; }
        public decimal TotalRentalValue { get; private set; }
    }
}
