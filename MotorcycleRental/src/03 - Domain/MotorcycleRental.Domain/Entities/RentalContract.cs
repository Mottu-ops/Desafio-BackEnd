namespace MotorcycleRental.Domain.Entities
{
    public class RentalContract : AggregateRoot
    {
        protected RentalContract() { }
        public RentalContract(Guid deliverymanId, Guid rentanPlanId, DateTime startDate, DateTime expectedEndDate)
        {
            Id = Guid.NewGuid();
            DeliverymanId = deliverymanId;
            RentanPlanId = rentanPlanId;
            StartDate = startDate;
            ExpectedEndDate = expectedEndDate;
            WasReturned = true;
        }

        public Guid DeliverymanId { get; private set; }
        public Guid RentanPlanId { get; private set; }
        public Guid MotorcycleId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool WasReturned { get; private set; }
        public decimal RentalValue { get; private set; }
        public decimal AdditionalFineValue { get; private set; }
        public decimal AdditionalDailyValue { get; private set; }
        public decimal TotalRentalValue { get; private set; }
        public Deliveryman Deliveryman { get; private set; }
        public RentalPlan Plan { get; private set; }
        public Motorcycle Motorcycle { get; private set; }

        //public void SetPlan(RentalPlan plan)
        //{
        //    Plan = plan;
        //}

        //public void SetDeliveryman(Deliveryman deliveryman)
        //{
        //    Deliveryman = deliveryman;
        //}
        //public void SetMotorcycle(Motorcycle motorcycle)
        //{
        //    Motorcycle = motorcycle;
        //}

        public void SetExpectedEndDate(DateTime expectedEndDate)
        {
            ExpectedEndDate = expectedEndDate;
        }

        public void SetRentalValue(decimal rentalValue)
        {
            RentalValue = rentalValue;// Plan.Days * Plan.DayValue;
        }



        public void FinalizeRental(RentalPlan plan, DateTime? endDate = null)
        {

            EndDate = endDate.HasValue ? endDate.Value : DateTime.UtcNow;

            TotalRentalValue = plan.Days * plan.DayValue;

            if (EndDate < ExpectedEndDate)
            {
                decimal fine = CalculateFine(plan, EndDate);
                AdditionalFineValue = fine;
                TotalRentalValue = RentalValue + fine;
            }
            else if (EndDate > ExpectedEndDate)
            {
                int additionalDays = (EndDate - ExpectedEndDate).Days;
                AdditionalDailyValue = (additionalDays * 50);
                TotalRentalValue = RentalValue + AdditionalDailyValue;
            }
        }

        private decimal CalculateFine(RentalPlan plan, DateTime endDate)
        {
            //TimeSpan period = ExpectedEndDate - endDate;
            //int daysRemaining = period.Days;
            int daysRemaining = (ExpectedEndDate - endDate).Days;

            decimal valueRental = plan.DayValue * daysRemaining;
            decimal fine = 0;

            switch (plan.Days)
            {
                case 7:
                    fine = valueRental * (plan.PercentageFine / 100);
                    break;
                case 15:
                    fine = valueRental * (plan.PercentageFine / 100);
                    break;
                default:
                    break;
            }
            return fine;
        }
    }
}
