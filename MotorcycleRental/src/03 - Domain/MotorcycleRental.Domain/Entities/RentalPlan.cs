using MotorcycleRental.Domain.Events;

namespace MotorcycleRental.Domain.Entities
{
    /// <summary>
    /// Entity responsible for managing Rental Plan
    /// </summary>
    public class RentalPlan : AggregateRoot
    {
        public RentalPlan(string descrition, int days, decimal dayValue, decimal percentageFine, decimal additionalValueDaily)
        {
            Id = Guid.NewGuid();
            Descrition = descrition;
            Days = days;
            DayValue = dayValue;
            PercentageFine = percentageFine;
            AdditionalValueDaily = additionalValueDaily;
            IsActived = true;
        }

        public string Descrition { get; private set; }
        public int Days { get; private set; }
        public decimal DayValue { get; private set; }
        /// <summary>
        /// When the informed date is less than the expected end date, the daily rate and an additional fine will be charged.
        /// * For a 7-day plan, the fine is 20% of the value of the daily rates not paid.
        /// * For a 15-day plan, the fine is 40% of the value of the daily rates not paid.
        /// </summary>
        public decimal PercentageFine { get; private set; }

        /// <summary>
        /// When the informed date is higher than the expected end date, an additional amount of R$50.00 will be charged per additional night.
        /// * R$50.00 per additional night.
        /// </summary>
        public decimal AdditionalValueDaily { get; private set; }
        public ICollection<RentalContract> RentalContracts { get; set; }

        public void SetCreatedAtDate()
        {
            CreatedAt = DateTime.UtcNow;
            AddEvent(new RentalPlanCreated(Id, Descrition, Days, DayValue, PercentageFine, AdditionalValueDaily, CreatedAt, IsActived));
        }
    }
}
