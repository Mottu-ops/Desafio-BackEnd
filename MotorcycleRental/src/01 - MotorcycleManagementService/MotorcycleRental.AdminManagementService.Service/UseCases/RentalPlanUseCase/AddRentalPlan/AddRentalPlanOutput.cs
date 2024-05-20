namespace MotorcycleRental.AdminManagementService.Service.UseCases.RentalPlanUseCase.AddRentalPlan
{
    public class AddRentalPlanOutput
    {
        public Guid Id { get; set; }
        public string Descrition { get; set; }
        public int Days { get; set; }
        public decimal DayValue { get; set; }
        public decimal PercentageFine { get; set; }
        public decimal AdditionalValueDaily { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActived { get; set; }
    }
}
