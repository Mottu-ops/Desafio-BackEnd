namespace MotorcycleRental.Domain.Entities
{
    public class Deliveryman : AggregateRoot
    {
        protected Deliveryman() { }

        public Deliveryman(Guid id, string name, string email, string cNPJ, DateTime dateOfBirth, string driverLicenseNumber,
                           string driverLicenseType)
        {
            Id = id;
            Name = name;
            Email = email;
            CNPJ = cNPJ;
            DateOfBirth = dateOfBirth;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = driverLicenseType;
            IsActived = true;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string DriverLicenseNumber { get; private set; }
        public string DriverLicenseType { get; private set; }
        public string CNHImageUrl { get; private set; }
        public ICollection<RentalContract> RentalContracts { get; set; }

        public void SetNewId()
        {
            Id = Guid.NewGuid();
        }
        public void SetCnhImageUrl(string utl)
        {
            CNHImageUrl = utl;
        }
    }
}