using System.Numerics;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class SimulationDto
    {
        public SimulationDto(DateTime startDate, DateTime endDate)
        {
            ContactDate = startDate;
            StartDate = startDate.AddDays(1);
            EndDate = endDate;
            Plans = new List<PlanDto>();
        }

        public DateTime ContactDate { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PlanDto> Plans { get; set; }
    }

    public class PlanDto
    {
        public PlanDto(Guid plaId, string description, decimal dayValue, decimal rentalValue, decimal additionalFineValue, decimal additionalDailyValue, decimal totalRentalValue)
        {
            PlanId = plaId;
            Description = description;
            DayValue = dayValue;
            RentalValue = rentalValue;
            AdditionalFineValue = additionalFineValue;
            AdditionalDailyValue = additionalDailyValue;
            TotalRentalValue = totalRentalValue;
            Observation = new List<string>();
        }

        public Guid PlanId { get; set; }
        public string Description { get; private set; }
        public decimal DayValue { get; private set; }
        public decimal RentalValue { get; private set; }
        public decimal AdditionalFineValue { get; private set; }
        public decimal AdditionalDailyValue { get; private set; }
        public decimal TotalRentalValue { get; private set; }
        public List<string> Observation { get; set; }
    }
}
