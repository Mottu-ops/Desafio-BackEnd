namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.AddRentalPlan
{
    public class AddRentalPlanInput
    {
        public string Descrition { get; set; }
        public int Days { get; set; }
        public decimal DayValue { get; set; }
        public decimal PercentageFine { get; set; }
        public decimal AdditionalValueDaily { get; set; }
    }
}
