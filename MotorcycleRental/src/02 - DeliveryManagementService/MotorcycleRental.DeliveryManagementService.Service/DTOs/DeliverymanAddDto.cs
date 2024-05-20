using MotorcycleRental.DeliveryManagementService.Service.Exceptions;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs
{
    public class DeliverymanAddDto
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string DriverLicenseType { get; set; }
        public byte[] CNHImageBase64 { get; set; }
        public string CNHImageUrl { get; private set; } = string.Empty;

        public void SetUrlImage(string url)
        {
            CNHImageUrl = url;
        }

        public void IsValidDriverLicenseType()
        {
            switch (DriverLicenseType)
            {
                case "A":
                case "B":
                case "AB":
                case "A+B":
                case "A + B":
                    break;
                default:
                    throw new InvalidDriverLicenseTypeException("Valid license types are A, B or both A+B");
                    break;
            }
        }
    }
}
