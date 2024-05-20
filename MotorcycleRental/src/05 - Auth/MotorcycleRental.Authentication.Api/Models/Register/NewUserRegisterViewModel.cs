namespace MotorcycleRental.Authentication.Api.Models.Register
{
    public class NewUserRegisterViewModel
    {
        public NewUserRegisterViewModel() { }

        public NewUserRegisterViewModel(Guid id, string email, string name, string cnpj,
                                        DateTime dateOfBirth, string driverLicenseNumber, string driverLicenseType)
        {
            Id = id;
            Email = email;
            Name = name;
            Cnpj = cnpj;
            DateOfBirth = dateOfBirth;
            DriverLicenseNumber = driverLicenseNumber;
            DriverLicenseType = driverLicenseType;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string DriverLicenseType { get; set; }
    }
}
