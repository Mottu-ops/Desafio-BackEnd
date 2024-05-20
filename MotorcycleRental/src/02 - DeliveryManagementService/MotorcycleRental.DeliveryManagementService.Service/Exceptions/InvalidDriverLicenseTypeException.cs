namespace MotorcycleRental.DeliveryManagementService.Service.Exceptions
{
    public class InvalidDriverLicenseTypeException : Exception
    {
        public InvalidDriverLicenseTypeException()
        {
        }

        public InvalidDriverLicenseTypeException(string message)
            : base(message)
        {
        }
    }
}