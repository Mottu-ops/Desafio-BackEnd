namespace MotorcycleRental.Domain.Events
{
    public class RentalPlanCreated : IDomainEvent
    {
        public RentalPlanCreated(Guid id, string descrition, int days, decimal dayValue, decimal percentageFine, decimal additionalValueDaily, DateTime createdAt, bool isActived)
        {
            Id = id;
            Descrition = descrition;
            Days = days;
            DayValue = dayValue;
            PercentageFine = percentageFine;
            AdditionalValueDaily = additionalValueDaily;
            CreatedAt = createdAt;
            IsActived = isActived;
        }

        public Guid Id { get; private set; }
        public string Descrition { get; private set; }
        public int Days { get; private set; }
        public decimal DayValue { get; private set; }
        public decimal PercentageFine { get; private set; }
        public decimal AdditionalValueDaily { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActived { get; private set; }
    }
}
