using System;


namespace MT.Backend.Challenge.Domain.Entities
{
    public class DeliveryDriver : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string DriversLicenseNumber { get; set; } = null!;
        public DriversLicenseCategory DriversLicenseCategory { get; set; }
        public DateTime? DriversLicenseValidDate{ get; set; }
        public string DriversLicenseImage { get; set; } = null!;
        public string DriversLicenseImageUrl { get; set; } = null!;

    }
}
