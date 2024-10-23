using System;

namespace MT.Backend.Challenge.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public string DeliveryDriverId { get; set; } = null!;
        public DeliveryDriver DeliveryDriver { get; set; } = null!;
        public string MotorcycleId { get; set; } = null!;
        public Motorcycle Motorcycle { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public string RentalCategoryId { get; set; } = null!;
        public RentalCategory RentalCategory { get; set; } = null!;
    }
}